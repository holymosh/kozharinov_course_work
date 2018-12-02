<template>
    <div id="InvestProject">
        <h1>Инвестиционные проекты</h1>
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
                              <v-text-field v-model="editedItem.name" label="Наименование"></v-text-field>
                          </v-flex>
                          <v-flex xs12 sm6 md4>
                              <v-text-field v-model="editedItem.startTime" label="Начало"></v-text-field>
                          </v-flex>
                          <v-flex xs12 sm6 md4>
                              <v-text-field v-model="editedItem.endTime" label="Окончание"></v-text-field>
                          </v-flex>
                          <v-flex xs12 sm6 md4>
                              <v-text-field v-model="editedItem.description" label="Описание"></v-text-field>
                          </v-flex>
                          <v-flex xs12 sm6 md4>
                              <v-text-field v-model="editedItem.cost" label="Стоимость"></v-text-field>
                          </v-flex>
                          <v-flex xs12 sm6 md4>
                              <v-text-field v-model="editedItem.customer" label="Заказчик"></v-text-field>
                          </v-flex>
                          <v-flex xs12 sm6 md4>
                              <v-text-field v-model="editedItem.executor" label="Исполнитель"></v-text-field>
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
        <v-data-table :headers="headers" :items="projects" :rows-per-page-items="[7]" class="elevation-1">
            <template slot="items" slot-scope="props">
                <td class="hidden">{{props.item.id}}</td>
                <td>{{props.item.name}}</td>
                <td>{{props.item.startTime}}</td>
                <td>{{props.item.endTime}}</td>
                <td>{{props.item.description}}</td>
                <td>{{props.item.cost}}</td>
                <td>{{props.item.customer}}</td>
                <td>{{props.item.executor}}</td>
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
      {text: 'Наименование', value: 'name'},
      {text: 'Начало', value: 'startTime'},
      {text: 'Окончание', value: 'endTime'},
      {text: 'Описание', value: 'description'},
      {text: 'Стоимость', value: 'cost'},
      {text: 'Заказчик', value: 'customer'},
      {text: 'Исполнитель', value: 'executor'},
    ],
    projects: [],
    editedIndex: -1,
    editedItem: {
      id: 0,
      name: '',
      startTime: '',
      endTime: '',
      description: '',
      cost: 0,
      customer: '',
      executor: ''
    },
    defaultItem: {
      id: 0,
      name: '',
      startTime: '',
      endTime: '',
      description: '',
      cost: 0,
      customer: '',
      executor: ''
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
      this.projects = [
        {id: 1, name: 'Инвест проект1', startTime: 2011, endTime: 2012, description: 'Описание', cost: 100000, customer: 'АО Кожевники', executor: 'ОА Кековники'},
        {id: 2, name: 'Инвест проект2', startTime: 2012, endTime: 2013, description: 'Описание', cost: 100000, customer: 'АО Кожевники', executor: 'ОА Кековники'},
        {id: 3, name: 'Инвест проект3', startTime: 2013, endTime: 2014, description: 'Описание', cost: 100000, customer: 'АО Кожевники', executor: 'ОА Кековники'},
        {id: 4, name: 'Инвест проект4', startTime: 2014, endTime: 2015, description: 'Описание', cost: 100000, customer: 'АО Кожевники', executor: 'ОА Кековники'},
        {id: 5, name: 'Инвест проект5', startTime: 2015, endTime: 2016, description: 'Описание', cost: 100000, customer: 'АО Кожевники', executor: 'ОА Кековники'},
        {id: 6, name: 'Инвест проект6', startTime: 2016, endTime: 2017, description: 'Описание', cost: 100000, customer: 'АО Кожевники', executor: 'ОА Кековники'},
        {id: 7, name: 'Инвест проект7', startTime: 2017, endTime: 2018, description: 'Описание', cost: 100000, customer: 'АО Кожевники', executor: 'ОА Кековники'},
        {id: 8, name: 'Инвест проект8', startTime: 2018, endTime: 2019, description: 'Описание', cost: 100000, customer: 'АО Кожевники', executor: 'ОА Кековники'},
        {id: 9, name: 'Инвест проект9', startTime: 2019, endTime: 2020, description: 'Описание', cost: 100000, customer: 'АО Кожевники', executor: 'ОА Кековники'},
        {id: 11, name: 'Инвест проект10', startTime: 2020, endTime: 2021, description: 'Описание', cost: 100000, customer: 'АО Кожевники', executor: 'ОА Кековники'},
        {id: 12, name: 'Инвест проект11', startTime: 2021, endTime: 2022, description: 'Описание', cost: 100000, customer: 'АО Кожевники', executor: 'ОА Кековники'},
        {id: 13, name: 'Инвест проект12', startTime: 2022, endTime: 2023, description: 'Описание', cost: 100000, customer: 'АО Кожевники', executor: 'ОА Кековники'},
        {id: 14, name: 'Инвест проект13', startTime: 2023, endTime: 2024, description: 'Описание', cost: 100000, customer: 'АО Кожевники', executor: 'ОА Кековники'}

      ]
    },
    editItem (item) {
      this.editedIndex = this.projects.indexOf(item)
      this.editedItem = Object.assign({}, item)
      this.dialog = true
    },
    deleteItem (item) {
      const index = this.projects.indexOf(item)
      confirm('delete') && this.projects.splice(index, 1)
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
        Object.assign(this.projects[this.editedIndex], this.editedItem)
      } else {
        this.projects.push(this.editedItem)
      }
      this.close()
    }
  }
}
</script>
