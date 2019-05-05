<template>
    <div id="InvestProject">
        <h1>Инвестиционные проекты</h1>
      <v-dialog v-model="dialog" max-width="500px">
        <v-btn v-show="showEditable" slot="activator" color="primary" dark class="mb-2">Создать</v-btn>
        <v-card height="500px">
            <v-card-title>
                <span class="headline">{{editedIndex === -1 ? 'Создание' : 'Редактирование'}}</span>
            </v-card-title>

            <v-card-text>
                <v-container fluid grid-list-xl>
                  <v-layout wrap>
                      <v-flex class="hidden" xs12 sm6 md4>
                        <v-text-field v-model="editedItem.id" label="Id"></v-text-field>
                      </v-flex>
                      <v-flex xs12 sm6 md4>
                        <v-text-field v-model="editedItem.name" label="Наименование"></v-text-field>
                      </v-flex>
                      <v-flex xs12 sm6 md4>
                        <v-text-field v-model="editedItem.startYear" label="Начало"></v-text-field>
                      </v-flex>
                      <v-flex xs12 sm6 md4>
                        <v-text-field v-model="editedItem.endYear" label="Окончание"></v-text-field>
                      </v-flex>
                      <v-flex xs12 sm6 md4>
                        <v-text-field v-model="editedItem.description" label="Описание"></v-text-field>
                      </v-flex>
                      <v-flex xs12 sm6 md4>
                        <v-text-field v-model="editedItem.totalCost" label="Стоимость"></v-text-field>
                      </v-flex>
                      <v-flex xs12 sm6 md4>
                        <v-text-field v-model="editedItem.customer" label="Заказчик"></v-text-field>
                      </v-flex>
                      <v-flex xs12 sm6 md4>
                        <v-text-field v-model="editedItem.executor" label="Исполнитель"></v-text-field>
                      </v-flex>
                       <v-flex xs12 sm6 md4>
                        <v-select width="350px" attach v-model="editedItem.energeticsObject" :items="energeticsObjects" label="Объект" >
                          <template slot="selection" slot-scope="data">
                            {{data.item.name}}
                          </template>
                          <template slot="item" slot-scope="data">
                            {{data.item.name}}
                          </template>
                        </v-select>
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
              <td>{{props.item.startYear}}</td>
              <td>{{props.item.endYear}}</td>
              <td>{{props.item.description}}</td>
              <td>{{props.item.totalCost}}</td>
              <td>{{props.item.customer}}</td>
              <td>{{props.item.executor}}</td>
              <td>{{props.item.energeticsObject ? props.item.energeticsObject.name : ''}}</td>
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
    menu: false,
    modal: false,
    menu2: false,
    showEditable: window.role !== 'read',
    headers: [
      // {text: 'Id', value: 'id'},
      {text: 'Наименование', value: 'name'},
      {text: 'Начало', value: 'startYear'},
      {text: 'Окончание', value: 'endYear'},
      {text: 'Описание', value: 'description'},
      {text: 'Стоимость', value: 'totalCost'},
      {text: 'Заказчик', value: 'customer'},
      {text: 'Исполнитель', value: 'executor'},
      {text: 'Объект', value: 'energeticsObject'}
    ],
    projects: [],
    energeticsObjects: [],
    editedIndex: -1,
    editedItem: {
      id: 0,
      name: '',
      startYear: '',
      endYear: '',
      description: '',
      totalCost: 0,
      customer: '',
      executor: '',
      energeticsObject: ''
    },
    defaultItem: {
      id: 0,
      name: '',
      startYear: '',
      endYear: '',
      description: '',
      totalCost: 0,
      customer: '',
      executor: '',
      energeticsObject: ''
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
    initializeObjects () {
      var xhr = new XMLHttpRequest()
      xhr.open('GET', 'http://localhost:44389/api/object/all', false)
      xhr.send(null)
      this.energeticsObjects = JSON.parse(xhr.responseText)
      console.log(xhr.responseText)
    },
    initializeProjects () {
      var xhr = new XMLHttpRequest()
      xhr.open('GET', 'http://localhost:44389/api/project', false)
      xhr.send(null)
      this.projects = JSON.parse(xhr.responseText)
      console.log(xhr.responseText)
    },
    initialize () {
      this.initializeObjects()
      this.initializeProjects()
    },
    editItem (item) {
      this.editedIndex = this.projects.indexOf(item)
      this.editedItem = Object.assign({}, item)
      this.dialog = true
    },
    deleteItem (item) {
      const index = this.projects.indexOf(item)
      confirm('delete') && this.projects.splice(index, 1)
      var xhr = new XMLHttpRequest()
      xhr.open('DELETE', 'http://localhost:44389/api/project/' + item.id, false)
      xhr.setRequestHeader('Content-Type', 'application/json')
      xhr.send(null)
    },
    close () {
      this.dialog = false
      setTimeout(() => {
        this.editedItem = Object.assign({}, this.defaultItem)
        this.editedIndex = -1
      }, 300)
    },
    save () {
      var data = JSON.stringify(this.editedItem)
      var xhr = new XMLHttpRequest()
      if (this.editedIndex > -1) {
        Object.assign(this.projects[this.editedIndex], this.editedItem)
        xhr.open('PUT', 'http://localhost:44389/api/project', false)
        xhr.setRequestHeader('Content-Type', 'application/json')
        xhr.send(data)
      } else {
        xhr.open('POST', 'http://localhost:44389/api/project', false)
        xhr.setRequestHeader('Content-Type', 'application/json')
        console.log(this.editedItem)
        xhr.send(data)
        this.editedItem.id = xhr.responseText
        this.projects.push(this.editedItem)
        console.log(this.editedItem.id)
      }
      this.close()
    }
  }
}
</script>
