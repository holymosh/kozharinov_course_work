<template>
  <div class="hello" id="HelloWorld">
    <h1>Типы объектов</h1>
    <v-dialog v-model="dialog" max-width="500px">
          <v-btn v-show="showEditable" slot="activator" color="primary" dark class="mb-2">Создать</v-btn>
          <v-card height="400px">
              <v-card-title>
                  <span class="headline">{{editedIndex === -1 ? 'Создание' : 'Редактирование'}}</span>
              </v-card-title>

              <v-card-text>
                  <v-container fluid grid-list-xl>
                      <v-layout wrap>
                          <v-flex class="hidden" xs12 sm6 md4>
                              <v-text-field v-model="editedItem.id" label="Id" ></v-text-field>
                          </v-flex>
                          <v-flex xs12 sm6 md4>
                              <v-text-field v-model="editedItem.title" label="Наименование"></v-text-field>
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
        <v-data-table :headers="headers" :items="objectTypes" :rows-per-page-items="[7]" class="elevation-1">
            <template slot="items" slot-scope="props">
                <td class="hidden">{{props.item.id}}</td>
                <td>{{props.item.title}}</td>
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

<script>
export default {
  data: () => ({
    dialog: false,
    showEditable: window.role !== 'read',
    rowsPerPageItems: [20],
    headers: [
      {text: 'Наименование', value: 'title'}
    ],
    objectTypes: [],
    editedIndex: -1,
    editedItem: {
      id: 0,
      title: ''
    },
    defaultItem: {
      id: 0,
      title: ''
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
      var xhr = new XMLHttpRequest()
      xhr.open('GET', 'https://localhost:44389/api/type', false)
      xhr.send(null)
      this.objectTypes = JSON.parse(xhr.responseText)
    },
    editItem (item) {
      this.editedIndex = this.objectTypes.indexOf(item)
      console.log(this.editedIndex)
      this.editedItem = Object.assign({}, item)
      this.dialog = true
    },
    deleteItem (item) {
      const index = this.objectTypes.indexOf(item)
      confirm('delete') && this.objectTypes.splice(index, 1)
      var xhr = new XMLHttpRequest()
      xhr.open('DELETE', 'https://localhost:44389/api/type/' + item.id, false)
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
      console.log('edited index - ' + this.editedIndex)
      if (this.editedIndex > 0) {
        Object.assign(this.objectTypes[this.editedIndex], this.editedItem)
        xhr.open('PUT', 'https://localhost:44389/api/type', false)
        xhr.setRequestHeader('Content-Type', 'application/json')
        xhr.send(data)
      } else {
        console.log(this.editedIndex)
        xhr.open('POST', 'https://localhost:44389/api/type', false)
        xhr.setRequestHeader('Content-Type', 'application/json')
        xhr.send(data)
        this.editedItem.id = xhr.responseText
        this.objectTypes.push(this.editedItem)
        console.log(this.editedItem.id)
      }
      this.close()
    }
  }
}
</script>

<!-- Add "scoped" attribute to limit CSS to this component only -->
<style scoped>
#HelloWorld{
}
h1{
  text-align: center;
  font-family: Arial, Helvetica, sans-serif;
}
h2{
  padding-bottom: 5px;
  padding-left: 1%;
}
li{
  font-size: 12pt
}
</style>
