<template>
    <div id="Holding">
        <h1>Холдинги</h1>
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
                          <v-flex xs12 sm6>
                              <!-- <v-text-field v-model="editedItem.parent" label="Родительский холдинг"></v-text-field> -->
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
                <td>{{props.item.parent.name ? props.item.parent.name :props.item.parent }}</td>
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
#Holding{
}
h1{
    text-align: center;
    font-family: Arial, Helvetica, sans-serif;
    padding: 2% 0
}
.hidden{
  display: none;
}
</style>
<script>
export default {
  data: () => ({
    dialog: false,
    rowsPerPageItems: [20],
    headers: [
      // {text: 'Id', value: 'id'},
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
      this.holdings = [
        {id: 1, name: 'Электроэнергетика РФ', parent: ''},
        {id: 2, name: 'АО Бийскэнерго', parent: 'Электроэнергетика РФ'},
        {id: 3, name: 'БЭСК', parent: 'Электроэнергетика РФ'},
        {id: 4, name: 'БЭС', parent: 'Электроэнергетика РФ'},
        {id: 5, name: 'АО Волга', parent: 'Электроэнергетика РФ'},
        {id: 6, name: 'Гор электросеть', parent: 'Электроэнергетика РФ'},
        {id: 8, name: 'Гор электросеть', parent: 'Электроэнергетика РФ'},
        {id: 10, name: 'Гор электросеть', parent: 'Электроэнергетика РФ'},
        {id: 9, name: 'Гор электросеть', parent: 'Электроэнергетика РФ'},
        {id: 11, name: 'Гор электросеть', parent: 'Электроэнергетика РФ'},
        {id: 12, name: 'Гор электросеть', parent: 'Электроэнергетика РФ'},
        {id: 13, name: 'Гор электросеть', parent: 'Электроэнергетика РФ'},
        {id: 15, name: 'Гор электросеть', parent: 'Электроэнергетика РФ'},
        {id: 7, name: 'Евросиб энерго', parent: 'Электроэнергетика РФ'}
      ]
    },
    editItem (item) {
      this.editedIndex = this.holdings.indexOf(item)
      this.editedItem = Object.assign({}, item)
      this.dialog = true
    },
    deleteItem (item) {
      const index = this.holdings.indexOf(item)
      confirm('delete') && this.holdings.splice(index, 1)
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
        Object.assign(this.holdings[this.editedIndex], this.editedItem)
      } else {
        this.holdings.push(this.editedItem)
      }
      this.close()
    }
  }
}
</script>
