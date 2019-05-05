<template>
    <div id="Object">
        <h1>Объекты электроэнергетики</h1>
      <v-dialog class="flo" v-model="dialog" max-width="700px">
          <v-btn slot="activator" color="primary" v-show="showEditable" dark class="mb-2">Создать</v-btn>
          <v-card height="400px">
              <v-card-title>
                  <span class="headline">{{editedIndex === -1 ? 'Создание' : 'Редактирование'}}</span>
              </v-card-title>

              <v-card-text>
                  <v-container grid-list-md>
                      <v-layout wrap>
                          <v-flex class="hidden" xs12 sm6 md4>
                              <v-text-field v-model="editedItem.id" label="Id"></v-text-field>
                          </v-flex>
                          <v-flex xs12 sm6 md4>
                              <v-text-field v-model="editedItem.name" label="Наименование"></v-text-field>
                          </v-flex>
                          <v-flex xs12 sm6 md4>
                              <v-select width="350px" attach v-model="editedItem.parent" :items="objects" label="Родительский объект" >
                                <template slot="selection" slot-scope="data">
                                  {{data.item.name}}
                                </template>
                                <template slot="item" slot-scope="data">
                                  {{data.item.name}}
                                </template>
                              </v-select>
                          </v-flex>
                          <v-flex xs12 sm6 md4>
                              <v-select width="350px" attach v-model="editedItem.subject" :items="subjects" label="Субъект" >
                                <template slot="selection" slot-scope="data">
                                  {{data.item.name}}
                                </template>
                                <template slot="item" slot-scope="data">
                                  {{data.item.name}}
                                </template>
                              </v-select>
                          </v-flex>
                          <v-flex xs12 sm6 md4>
                              <v-text-field v-model="editedItem.address" label="Адрес"></v-text-field>
                          </v-flex>
                          <v-flex xs12 sm6 md4>
                              <!-- <v-text-field v-model="editedItem.objType" label="Тип объекта"></v-text-field> -->
                              <v-select width="350px" attach v-model="editedItem.objType" :items="objectTypes.map(function(arg){return arg.title})" label="Тип"></v-select>
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
        <v-data-table :headers="headers" :rows-per-page-items="[7]" :items="objects" class="elevation-1">
            <template slot="items" slot-scope="props">
                <td class="hidden">{{props.item.id}}</td>
                <td>{{props.item.name}}</td>
                <td>{{props.item.parent? props.item.parent.name : '' }}</td>
                <td>{{props.item.subject? props.item.subject.name : ''}}</td>
                <td>{{props.item.address}}</td>
                <td>{{props.item.objType}}</td>
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
#Subject{
}
h1{
    text-align: center;
    font-family: Arial, Helvetica, sans-serif;
    margin-top:2%;
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
      {text: 'Наименование', value: 'name'},
      {text: 'Родительский объект', value: 'parent'},
      {text: 'Субъект', value: 'subject'},
      {text: 'Адрес', value: 'address'},
      {text: 'Тип', value: 'objType'}
    ],
    objects: [],
    objectTypes: [],
    subjects: [],
    editedIndex: -1,
    editedItem: {
      id: 0,
      name: '',
      parent: '',
      subject: '',
      address: '',
      objType: ''
    },
    defaultItem: {
      id: 0,
      name: '',
      parent: '',
      subject: '',
      address: '',
      objType: ''
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
    initializeSubjects () {
      var xhr = new XMLHttpRequest()
      xhr.open('GET', 'http://localhost:44389/api/subject/all', false)
      xhr.send(null)
      this.subjects = JSON.parse(xhr.responseText)
      console.log(xhr.responseText)
    },
    initializeObjects () {
      var xhr = new XMLHttpRequest()
      xhr.open('GET', 'http://localhost:44389/api/object', false)
      xhr.send(null)
      this.objects = JSON.parse(xhr.responseText)
    },
    initializeTypes () {
      var xhr = new XMLHttpRequest()
      xhr.open('GET', 'http://localhost:44389/api/type', false)
      xhr.send(null)
      this.objectTypes = JSON.parse(xhr.responseText)
    },
    initialize () {
      this.initializeSubjects()
      this.initializeObjects()
      this.initializeTypes()
    },
    editItem (item) {
      this.editedIndex = this.objects.indexOf(item)
      this.editedItem = Object.assign({}, item)
      this.dialog = true
    },
    deleteItem (item) {
      const index = this.objects.indexOf(item)
      confirm('delete') && this.objects.splice(index, 1)
      var xhr = new XMLHttpRequest()
      xhr.open('DELETE', 'http://localhost:44389/api/object/' + item.id, false)
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
        Object.assign(this.objects[this.editedIndex], this.editedItem)
        xhr.open('PUT', 'http://localhost:44389/api/object', false)
        xhr.setRequestHeader('Content-Type', 'application/json')
        xhr.send(data)
      } else {
        xhr.open('POST', 'http://localhost:44389/api/object', false)
        xhr.setRequestHeader('Content-Type', 'application/json')
        console.log(this.editedItem)
        xhr.send(data)
        this.editedItem.id = xhr.responseText
        this.objects.push(this.editedItem)
        console.log(this.editedItem.id)
      }
      this.close()
    }
  }
}
</script>
