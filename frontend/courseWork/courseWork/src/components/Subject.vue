<template>
    <div id="Subject">
        <h1>Субъекты электроэнергетики</h1>
      <v-dialog class="flo" v-model="dialog" max-width="600px">
          <v-btn v-show="showEditable" slot="activator" color="primary" dark class="mb-2">Создать</v-btn>
          <v-card height="580px">
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
                              <v-text-field v-model="editedItem.okved" label="ОКВЕД"></v-text-field>
                          </v-flex>
                          <v-flex xs12 sm6 md4>
                              <v-text-field v-model="editedItem.inn" label="ИНН"></v-text-field>
                          </v-flex>
                          <v-flex xs12 sm6 md4>
                              <v-text-field v-model="editedItem.address" label="Адрес"></v-text-field>
                          </v-flex>
                          <v-flex xs12 sm6 md4>
                              <v-text-field v-model="editedItem.postAddress" label="Почтовый адрес"></v-text-field>
                          </v-flex>
                          <v-flex xs12 sm6 md4>
                              <v-text-field v-model="editedItem.kpp" label="КПП"></v-text-field>
                          </v-flex>
                          <v-flex xs12 sm6 md4>
                              <v-select attach v-model="editedItem.holding" label="Холдинг"
                               :items="holdings">
                               <template slot="selection" slot-scope="data">
                                  {{data.item.name}}
                                </template>
                                <template slot="item" slot-scope="data">
                                  {{data.item.name}}
                                </template>
                               </v-select>
                          </v-flex>
                          <v-flex xs12 sm6 md4>
                              <v-text-field v-model="editedItem.email" label="E-mail"></v-text-field>
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
        <v-data-table :headers="headers" :rows-per-page-items="[7]" :items="subjects" class="elevation-1">
            <template slot="items" slot-scope="props">
                <td class="hidden">{{props.item.id}}</td>
                <td>{{props.item.name}}</td>
                <td>{{props.item.okved}}</td>
                <td>{{props.item.inn}}</td>
                <td>{{props.item.address}}</td>
                <td>{{props.item.postAddress}}</td>
                <td>{{props.item.kpp}}</td>
                <td>{{props.item.holding ? props.item.holding.name : ''}}</td>
                <td>{{props.item.email}}</td>
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
      {text: 'Оквед', value: 'okved'},
      {text: 'ИНН', value: 'inn'},
      {text: 'Адрес', value: 'address'},
      {text: 'Почтовый адрес', value: 'postAddress'},
      {text: 'КПП', value: 'kpp'},
      {text: 'Холдинг', value: 'holding'},
      {text: 'E-mail', value: 'email'}
    ],
    subjects: [],
    holdings: [],
    editedIndex: -1,
    editedItem: {
      id: 0,
      name: '',
      okved: '',
      inn: '',
      address: '',
      postAddress: '',
      kpp: '',
      holding: '',
      email: ''
    },
    defaultItem: {
      id: 0,
      name: '',
      okved: '',
      inn: '',
      address: '',
      postAddress: '',
      kpp: '',
      holding: '',
      email: ''
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
      this.initializeHoldings()
      this.initializeSubjects()
    },
    initializeHoldings () {
      var xhr = new XMLHttpRequest()
      xhr.open('GET', 'https://localhost:44389/api/holding', false)
      xhr.send(null)
      this.holdings = JSON.parse(xhr.responseText)
      console.log(this.holdings)
    },
    initializeSubjects () {
      var xhr = new XMLHttpRequest()
      xhr.open('GET', 'https://localhost:44389/api/subject', false)
      xhr.send(null)
      this.subjects = JSON.parse(xhr.responseText)
      console.log(this.subjects)
    },
    editItem (item) {
      this.editedIndex = this.subjects.indexOf(item)
      this.editedItem = Object.assign({}, item)
      this.dialog = true
    },
    deleteItem (item) {
      const index = this.subjects.indexOf(item)
      confirm('delete') && this.subjects.splice(index, 1)
      var xhr = new XMLHttpRequest()
      xhr.open('DELETE', 'https://localhost:44389/api/subject/' + item.id, false)
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
        Object.assign(this.subjects[this.editedIndex], this.editedItem)
        xhr.open('PUT', 'https://localhost:44389/api/subject', false)
        xhr.setRequestHeader('Content-Type', 'application/json')
        xhr.send(data)
      } else {
        xhr.open('POST', 'https://localhost:44389/api/subject', false)
        xhr.setRequestHeader('Content-Type', 'application/json')
        console.log(this.editedItem)
        xhr.send(data)
        this.editedItem.id = xhr.responseText
        this.subjects.push(this.editedItem)
        console.log(this.editedItem.id)
      }
      this.close()
    }
  }
}
</script>
