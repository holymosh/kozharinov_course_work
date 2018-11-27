<template>
    <div id="Holding">
        <h1>Холдинги</h1>
        <v-toolbar flat color="white">
            <v-divider
        class="mx-2"
        inset
        vertical
      ></v-divider>
      <v-spacer></v-spacer>
      <v-dialog class="flo" v-model="dialog" max-width="500px">
          <v-btn slot="activator" color="primary" dark class="mb-2">Создать</v-btn>
          <v-card>
              <v-card-title>
                  <span class="headline">Новая запись</span>
              </v-card-title>

              <v-card-text>
                  <v-container grid-list-md>
                      <v-layout wrap>
                          <v-flex xs12 sm6 md4>
                              <v-text-field v-model="editedItem.id" label="Id"></v-text-field>
                          </v-flex>
                          <v-flex xs12 sm6 md4>
                              <v-text-field v-model="editedItem.name" label="Наименование"></v-text-field>
                          </v-flex>
                          <v-flex xs12 sm6 md4>
                              <v-text-field v-model="editedItem.parent" label="Родительский холдинг"></v-text-field>
                          </v-flex>
                      </v-layout>
                  </v-container>
              </v-card-text>

              <v-card-actions>
                  <v-spacer></v-spacer>
                  <v-btn color="blue darkgen-1" flat @click="close">Закрыть</v-btn>
                  <v-btn color="blue darkgen-1" flat @click="close">Сохранить</v-btn>
              </v-card-actions>
          </v-card>
        </v-dialog>
        </v-toolbar>
        <v-data-table :headers="headers" :items="subjects" class="elevation-1">
            <template slot="items" slot-scope="props">
                <td>{{props.item.id}}</td>
                <td>{{props.item.name}}</td>
                <td>{{props.item.parent}}</td>
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
#Subject{
}
h1{
    text-align: center;
    font-family: Arial, Helvetica, sans-serif;
    padding-bottom:2%;
}
</style>
<script>
export default {
  data: () => ({
    dialog: false,
    headers: [
      {text: 'Id', value: 'id'},
      {text: 'Наименование', value: 'name'},
      {text: 'Родительский холдинг', value: 'parent'}
    ],
    objects: [],
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
      this.subjects = [
        {id: 1, name: 'Электроэнергетика РФ', parent: ''},
        {id: 2, name: 'АО Бийскэнерго', parent: 'Электроэнергетика РФ'},
        {id: 3, name: 'БЭСК', parent: 'Электроэнергетика РФ'},
        {id: 4, name: 'БЭС', parent: 'Электроэнергетика РФ'},
        {id: 5, name: 'АО Волга', parent: 'Электроэнергетика РФ'},
        {id: 6, name: 'Гор электросеть', parent: 'Электроэнергетика РФ'},
        {id: 7, name: 'Евросиб энерго', parent: 'Электроэнергетика РФ'}
      ]
    },
    editItem (item) {
      this.editedIndex = this.subjects.indexOf(item)
      this.editedItem = Object.assign({}, item)
      this.dialog = true
    },
    deleteItem (item) {
      const index = this.subjects.indexOf(item)
      confirm('delete') && this.subjects.splice(index, 1)
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
        Object.assign(this.subjects[this.editedIndex], this.editedItem)
      } else {
        this.subjects.push(this.editedItem)
      }
      this.close()
    }
  }
}
</script>
