<template>
    <div id="Checking">
        <h1>Выездные проверки инвестиционных проектов</h1>
      <v-dialog v-model="dialog" max-width="500px">
          <v-btn slot="activator" color="primary" dark class="mb-2">Создать</v-btn>
          <v-card height="500px">
              <v-card-title>
                  <span class="headline">{{editedIndex === -1 ? 'Создание' : 'Редактирование'}}</span>
              </v-card-title>

              <v-card-text>
                  <v-container fluid grid-list-xl>
                      <v-layout wrap>
                          <v-flex xs12 sm6 md4>
                              <v-text-field v-model="editedItem.id" label="Id"></v-text-field>
                          </v-flex>
                          <v-flex xs12 sm6 md4>
                              <v-text-field v-model="editedItem.projectName" label="Инвест проект"></v-text-field>
                          </v-flex>
                          <v-flex xs12 sm6 md4>
                              <v-text-field v-model="editedItem.startTime" label="Начало"></v-text-field>
                          </v-flex>
                          <v-flex xs12 sm6 md4>
                              <v-text-field v-model="editedItem.endTime" label="Окончание"></v-text-field>
                          </v-flex>
                          <v-flex xs12 sm6 md4>
                              <v-text-field v-model="editedItem.type" label="Тип проверки"></v-text-field>
                          </v-flex>
                      </v-layout>
                  </v-container>
              </v-card-text>

              <v-card-actions>
                  <v-spacer></v-spacer>
                  <v-btn color="blue darkgen-1" flat @click="close">Закрыть</v-btn>
                  <v-btn color="blue darkgen-1" flat @click="save">Сохранить</v-btn>
              </v-card-actions>
          </v-card>
        </v-dialog>
        <v-data-table :headers="headers" :items="checkings" :rows-per-page-items="[7]" class="elevation-1">
            <template slot="items" slot-scope="props">
                <td class="hidden">{{props.item.id}}</td>
                <td>{{props.item.projectName}}</td>
                <td>{{props.item.startTime}}</td>
                <td>{{props.item.endTime}}</td>
                <td>{{props.item.type}}</td>
                <td class="justify-center layout px-0">
                    <v-icon small class="mr-2" @click="editItem(props.item)">edit</v-icon>
                    <v-icon small class="mr-2" @click="deleteItem(props.item)">delete</v-icon>
                </td>
            </template>
            <template slot="no-data">
                <v-btn color="primary" @click="initialize">Reset</v-btn>
            </template>
        </v-data-table>
    </div>
</template>
<style>
h1{
    text-align: center;
    font-family: Arial, Helvetica, sans-serif;
    margin-top: 2%
}
.hidden{
  display: none;
}
</style>
<script>
export default {
  data: () => ({
    dialog: false,
    headers: [
      // {text: 'Id', value: 'id'},
      {text: 'Инвест проект', value: 'projectName'},
      {text: 'Начало', value: 'startTime'},
      {text: 'Окончание', value: 'endTime'},
      {text: 'Тип проверки', value: 'type'}
    ],
    checkings: [],
    editedIndex: -1,
    editedItem: {
      id: 0,
      projectName: '',
      startTime: '',
      endTime: '',
      type: ''
    },
    defaultItem: {
      id: 0,
      projectName: '',
      startTime: '',
      endTime: '',
      type: ''
    }
  }),

  computed: {
    formTitle () {
      return this.editedIndex === -1 ? 'New item' : 'Edit item'
    }
  },

  watch: {
    dialog (val) {
      val || this.close()
    }
  },
  created () {
    this.initialize()
  },
  methods: {
    initialize () {
      this.checkings = [
        {id: 1, projectName: 'ВП1', startTime: 2011, endTime: 2012, type: 'Плановая'},
        {id: 2, projectName: 'ВП2', startTime: 2012, endTime: 2013, type: 'Внеплановая'},
        {id: 3, projectName: 'ВП3', startTime: 2013, endTime: 2014, type: 'Плановая'},
        {id: 4, projectName: 'ВП5', startTime: 2014, endTime: 2015, type: 'Внеплановая'},
        {id: 5, projectName: 'ВП4', startTime: 2015, endTime: 2016, type: 'Плановая'},
        {id: 6, projectName: 'ВП6', startTime: 2016, endTime: 2017, type: 'Внеплановая'},
        {id: 7, projectName: 'ВП7', startTime: 2017, endTime: 2018, type: 'Плановая'},
        {id: 8, projectName: 'ВП8', startTime: 2018, endTime: 2019, type: 'Внеплановая'},
        {id: 9, projectName: 'ВП9', startTime: 2019, endTime: 2020, type: 'Плановая'},
        {id: 10, projectName: 'ВП10', startTime: 2021, endTime: 2022, type: 'Внеплановая'},
        {id: 11, projectName: 'ВП11', startTime: 2022, endTime: 2023, type: 'Плановая'},
        {id: 12, projectName: 'ВП12', startTime: 2023, endTime: 2024, type: 'Внеплановая'}
      ]
    },
    editItem (item) {
      this.editedIndex = this.checkings.indexOf(item)
      this.editedItem = Object.assign({}, item)
      this.dialog = true
    },
    deleteItem (item) {
      const index = this.checkings.indexOf(item)
      confirm('delete') && this.checkings.splice(index, 1)
    },
    close () {
      this.dialog = false
      setTimeout(() => {
        this.editedItem = Object.assign({}, this.defaultItem)
        this.editedIndex = -1
      }, 300)
    },
    save () {
      if (this.editedIndex > 1) {
        Object.assign(this.checkings[this.editedIndex], this.editedItem)
      } else {
        this.checkings.push(this.editedItem)
      }
      this.close()
    }
  }
}
</script>
