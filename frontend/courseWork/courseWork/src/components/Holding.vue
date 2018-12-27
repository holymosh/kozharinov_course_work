<template>
    <div id="Holding">
        <h1>Холдинги</h1>
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
                              <v-text-field v-model="editedItem.name" label="Наименование"></v-text-field>
                          </v-flex>
                          <v-flex xs12 sm6>
                              <v-select attach v-model="editedItem.parent" :items="holdings" label="Родительский холдинг">
                                <template slot="selection" slot-scope="data">
                                  {{data.item.name}}
                                </template>
                                <template slot="item" slot-scope="data">
                                  {{data.item.name}}
                                </template>
                              </v-select>
                              <!-- <v-overflow-btn :items="holdings" label="test"></v-overflow-btn> -->
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
        <v-data-table :headers="headers" :items="holdings" :rows-per-page-items="[7]" class="elevation-1">
            <template slot="items" slot-scope="props">
                <td class="hidden">{{props.item.id}}</td>
                <td>{{props.item.name}}</td>
                <td>{{props.item.parent ? props.item.parent.name :'' }}</td>
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
#Holding{
}
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
    rowsPerPageItems: [20],
    headers: [
      {text: 'Наименование', value: 'name'},
      {text: 'Родительский холдинг', value: 'parent'}
    ],
    holdings: [],
    editedIndex: -1,
    editedItem: {
      id: 0,
      name: '',
      parent: ''
    },
    defaultItem: {
      id: 0,
      name: '',
      parent: ''
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
      xhr.open('GET', 'https://localhost:44389/api/holding', false)
      xhr.send(null)
      this.holdings = JSON.parse(xhr.responseText)
    },
    editItem (item) {
      this.editedIndex = this.holdings.indexOf(item)
      this.editedItem = Object.assign({}, item)
      this.dialog = true
    },
    deleteItem (item) {
      const index = this.holdings.indexOf(item)
      confirm('delete') && this.holdings.splice(index, 1)
      var xhr = new XMLHttpRequest()
      xhr.open('DELETE', 'https://localhost:44389/api/holding/' + item.id, false)
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
      if (this.editedIndex > 1) {
        Object.assign(this.holdings[this.editedIndex], this.editedItem)
        xhr.open('PUT', 'https://localhost:44389/api/holding', false)
        xhr.setRequestHeader('Content-Type', 'application/json')
        xhr.send(data)
      } else {
        xhr.open('POST', 'https://localhost:44389/api/holding', false)
        xhr.setRequestHeader('Content-Type', 'application/json')
        xhr.send(data)
        this.editedItem.id = xhr.responseText
        this.holdings.push(this.editedItem)
        console.log(this.editedItem.id)
      }
      this.close()
    }
  }
}
</script>
