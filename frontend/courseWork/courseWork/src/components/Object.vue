<template>
    <div id="Object">
        <h1>Объекты электроэнергетики</h1>
      <v-dialog class="flo" v-model="dialog" max-width="500px">
          <v-btn slot="activator" color="primary" dark class="mb-2">Создать</v-btn>
          <v-card>
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
                              <!-- <v-text-field v-model="editedItem.parent" label="Родительский объект"></v-text-field> -->
                              <v-select attach v-model="editedItem.parent" :items="objects" label="Родительский объект" >
                                <template slot="selection" slot-scope="data">
                                  {{data.item.name}}
                                </template>
                                <template slot="item" slot-scope="data">
                                  {{data.item.name}}
                                </template>
                              </v-select>
                          </v-flex>
                          <v-flex xs12 sm6 md4>
                              <v-text-field v-model="editedItem.subject" label="Субъект"></v-text-field>
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
        <v-data-table :headers="headers" :items="objects" class="elevation-1">
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
        {id: 1, name: 'Пс 135 Кожевническая 1', parent: 'Кожевническая областная', subject: 'АО Кожевники', address: 'ул. КАСА д 1 кожевника', type: 'АЭС'},
        {id: 2, name: 'Пс 135 Кожевническая 2', parent: 'Кожевническая областная', subject: 'АО Кожевники', address: 'ул. КАСА д 1 кожевника', type: 'АЭС'},
        {id: 3, name: 'Пс 135 Кожевническая 3', parent: 'Кожевническая областная', subject: 'АО Кожевники', address: 'ул. КАСА д 1 кожевника', type: 'АЭС'},
        {id: 4, name: 'Пс 135 Кожевническая 4', parent: 'Кожевническая областная', subject: 'АО Кожевники', address: 'ул. КАСА д 1 кожевника', type: 'АЭС'},
        {id: 5, name: 'Пс 135 Кожевническая 5', parent: 'Кожевническая областная', subject: 'АО Кожевники', address: 'ул. КАСА д 1 кожевника', type: 'АЭС'},
        {id: 6, name: 'Пс 135 Кожевническая 6', parent: 'Кожевническая областная', subject: 'АО Кожевники', address: 'ул. КАСА д 1 кожевника', type: 'АЭС'},
        {id: 7, name: 'Пс 135 Кожевническая 7', parent: 'Кожевническая областная', subject: 'АО Кожевники', address: 'ул. КАСА д 1 кожевника', type: 'АЭС'},
        {id: 8, name: 'Пс 135 Кожевническая 8', parent: 'Кожевническая областная', subject: 'АО Кожевники', address: 'ул. КАСА д 1 кожевника', type: 'АЭС'},
        {id: 9, name: 'Пс 135 Кожевническая 9', parent: 'Кожевническая областная', subject: 'АО Кожевники', address: 'ул. КАСА д 1 кожевника', type: 'АЭС'},
        {id: 10, name: 'Пс 135 Кожевническая 10', parent: 'Кожевническая областная', subject: 'АО Кожевники', address: 'ул. КАСА д 1 кожевника', type: 'АЭС'},
        {id: 11, name: 'Пс 135 Кожевническая 11', parent: 'Кожевническая областная', subject: 'АО Кожевники', address: 'ул. КАСА д 1 кожевника', type: 'АЭС'}
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
