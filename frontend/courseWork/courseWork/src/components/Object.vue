<template>
    <div id="Object">
        <h1>Объекты электроэнергетики</h1>
      <v-dialog class="flo" v-model="dialog" max-width="700px">
          <v-btn slot="activator" color="primary" dark class="mb-2">Создать</v-btn>
          <v-card height="400px">
              <v-card-title>
                  <span class="headline">{{editedIndex === -1 ? 'Создание' : 'Редактирование'}}</span>
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
                              <!-- <v-text-field v-model="editedItem.subject" label="Субъект"></v-text-field> -->
                              <v-select attach v-model="editedItem.subject" :items="objects.map(obj => { return obj.subject.name ? obj.subject.name : obj.subject })">
                                <!-- <template slot="selection" slot-scope="data"> -->
                                  <!-- {{data.item}} -->
                                <!-- </template> -->
                                <!-- <template slot="item" slot-scope="data"> -->
                                  <!-- {{data.item}} -->
                                <!-- </template> -->
                              </v-select>
                          </v-flex>
                          <v-flex xs12 sm6 md4>
                              <v-text-field v-model="editedItem.address" label="Адрес"></v-text-field>
                          </v-flex>
                          <v-flex xs12 sm6 md4>
                              <v-text-field v-model="editedItem.type" label="Тип объекта"></v-text-field>
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
                <td>{{props.item.id}}</td>
                <td>{{props.item.name}}</td>
                <td>{{props.item.parent.name ? props.item.parent.name : props.item.parent }}</td>
                <td>{{props.item.subject}}</td>
                <td>{{props.item.address}}</td>
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
#Subject{
}
h1{
    text-align: center;
    font-family: Arial, Helvetica, sans-serif;
    margin-top:2%;
}
</style>
<script>
export default {
  data: () => ({
    dialog: false,
    headers: [
      {text: 'Id', value: 'id'},
      {text: 'Наименование', value: 'name'},
      {text: 'Родительский объект', value: 'parent'},
      {text: 'Субъект', value: 'subject'},
      {text: 'Адрес', value: 'address'},
      {text: 'Тип', value: 'type'}
    ],
    objects: [],
    editedIndex: -1,
    editedItem: {
      id: 0,
      name: '',
      parent: '',
      subject: '',
      address: '',
      type: ''
    },
    defaultItem: {
      id: 0,
      name: '',
      parent: '',
      subject: '',
      address: '',
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
      this.objects = [
        {id: 1, name: 'Пс 135 Станция 1', parent: 'Станция вышестоящая', subject: ' Электроэнергетика РФ', address: 'ул. Тестировщиков д 1  ', type: 'АЭС'},
        {id: 2, name: 'Пс 135 Станция 2', parent: 'Станция вышестоящая', subject: ' Электроэнергетика РФ', address: 'ул. Тестировщиков д 1  ', type: 'АЭС'},
        {id: 3, name: 'Пс 135 Станция 3', parent: 'Станция вышестоящая', subject: ' Электроэнергетика РФ', address: 'ул. Тестировщиков д 1  ', type: 'АЭС'},
        {id: 4, name: 'Пс 135 Станция 4', parent: 'Станция вышестоящая', subject: ' Электроэнергетика РФ', address: 'ул. Тестировщиков д 1  ', type: 'АЭС'},
        {id: 5, name: 'Пс 135 Станция 5', parent: 'Станция вышестоящая', subject: ' Электроэнергетика РФ', address: 'ул. Тестировщиков д 1  ', type: 'АЭС'},
        {id: 6, name: 'Пс 135 Станция 6', parent: 'Станция вышестоящая', subject: ' Электроэнергетика РФ', address: 'ул. Тестировщиков д 1  ', type: 'АЭС'},
        {id: 7, name: 'Пс 135 Станция 7', parent: 'Станция вышестоящая', subject: ' Электроэнергетика РФ', address: 'ул. Тестировщиков д 1  ', type: 'АЭС'},
        {id: 8, name: 'Пс 135 Станция 8', parent: 'Станция вышестоящая', subject: ' Электроэнергетика РФ', address: 'ул. Тестировщиков д 1  ', type: 'АЭС'},
        {id: 9, name: 'Пс 135 Станция 9', parent: 'Станция вышестоящая', subject: ' Электроэнергетика РФ', address: 'ул. Тестировщиков д 1  ', type: 'АЭС'},
        {id: 10, name: 'Пс 135 Станция 10', parent: 'Станция вышестоящая', subject: ' Электроэнергетика РФ', address: 'ул. Тестировщиков д 1  ', type: 'АЭС'},
        {id: 11, name: 'Пс 135 Станция 11', parent: 'Станция вышестоящая', subject: ' Электроэнергетика РФ', address: 'ул. Тестировщиков д 1  ', type: 'АЭС'}
      ]
    },
    editItem (item) {
      this.editedIndex = this.objects.indexOf(item)
      this.editedItem = Object.assign({}, item)
      this.dialog = true
    },
    deleteItem (item) {
      const index = this.objects.indexOf(item)
      confirm('delete') && this.objects.splice(index, 1)
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
        Object.assign(this.objects[this.editedIndex], this.editedItem)
      } else {
        this.objects.push(this.editedItem)
      }
      this.close()
    }
  }
}
</script>
