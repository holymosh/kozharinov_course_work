<template>
    <div id="Equipment">
        <h1>Оборудование проектов</h1>
      <v-dialog v-model="dialog" max-width="500px">
          <v-btn v-show="showEditable" slot="activator" color="primary" dark class="mb-2">Создать</v-btn>
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
                              <v-text-field v-model="editedItem.state" label="Состояние"></v-text-field>
                          </v-flex>
                          <v-flex xs12 sm6 md4>
                              <v-text-field v-model="editedItem.type" label="Производитель"></v-text-field>
                          </v-flex>
                          <v-flex xs12 sm6 md4>
                              <v-text-field v-model="editedItem.cost" label="Стоимость"></v-text-field>
                          </v-flex>
                          <v-flex xs12 sm6 md4>
                              <v-text-field v-model="editedItem.manufacturer" label="Заказчик"></v-text-field>
                          </v-flex>
                          <v-flex xs12 sm6 md4>
                              <v-text-field v-model="editedItem.project" label="Проект"></v-text-field>
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
        <v-data-table :headers="headers" :items="equipments" :rows-per-page-items="[7]" class="elevation-1">
            <template slot="items" slot-scope="props">
                <td class="hidden">{{props.item.id}}</td>
                <td>{{props.item.name}}</td>
                <td>{{props.item.state}}</td>
                <td>{{props.item.type}}</td>
                <td>{{props.item.manufacturer}}</td>
                <td>{{props.item.cost}}</td>
                <td>{{props.item.project}}</td>
                <td v-show="showEditable" class="justify-center layout px-0">
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
    showEditable: window.role !== 'read',
    headers: [
      // {text: 'Id', value: 'id'},
      {text: 'Наименование', value: 'name'},
      {text: 'Состояние', value: 'state'},
      {text: 'Тип оборудования', value: 'type'},
      {text: 'Производитель', value: 'manufacturer'},
      {text: 'Стоимость', value: 'cost'},
      {text: 'Проект', value: 'project'},
      {text: 'Примерная дата поломки', value: 'predicted'}
    ],
    equipments: [],
    editedIndex: -1,
    editedItem: {
      id: 0,
      name: '',
      state: '',
      type: '',
      manufacturer: '',
      cost: 0,
      project: '',
      predicted: new Date()
    },
    defaultItem: {
      id: 0,
      name: '',
      state: '',
      type: '',
      manufacturer: '',
      cost: 0,
      project: '',
      predicted: new Date()
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
      this.equipments = [
        {id: 1, name: 'Железка', state: 'В норме', cost: 100000, manufacturer: 'Siemens', project: 'Проект1'},
        {id: 2, name: 'Деталька', state: 'В норме', cost: 100000, manufacturer: 'Siemens', project: 'Проект2'},
        {id: 3, name: 'Турбина', state: 'Пора менять', cost: 100000, manufacturer: 'Siemens', project: 'Проект'},
        {id: 4, name: 'Болт', state: 'В норме', cost: 100000, manufacturer: 'Siemens', project: 'Проект'},
        {id: 5, name: 'Гайка', state: 'Сломано', cost: 100000, manufacturer: 'Siemens', project: 'Проект2'},
        {id: 6, name: 'Турбина', state: 'В норме', cost: 100000, manufacturer: 'Siemens', project: 'Проект3'},
        {id: 7, name: 'Деталька', state: 'В норме', cost: 100000, manufacturer: 'Siemens', project: 'Проект4'},
        {id: 8, name: 'Турбина', state: 'В норме', cost: 100000, manufacturer: 'Siemens', project: 'Проект1'},
        {id: 9, name: 'Железка', state: 'В норме', cost: 100000, manufacturer: 'Siemens', project: 'Проект2'},
        {id: 10, name: 'Деталька', state: 'В норме', cost: 100000, manufacturer: 'Siemens', project: 'Проект3'},
        {id: 11, name: 'Турбина', state: 'В норме', cost: 100000, manufacturer: 'Siemens', project: 'Проект4'}
      ]
    },
    editItem (item) {
      this.editedIndex = this.equipments.indexOf(item)
      this.editedItem = Object.assign({}, item)
      this.dialog = true
    },
    deleteItem (item) {
      const index = this.equipments.indexOf(item)
      confirm('delete') && this.equipments.splice(index, 1)
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
        Object.assign(this.equipments[this.editedIndex], this.editedItem)
      } else {
        this.equipments.push(this.editedItem)
      }
      this.close()
    }
  }
}
</script>
