<template>
    <div id="Subject">
        <h1>Субъекты электроэнергетики</h1>
      <v-dialog class="flo" v-model="dialog" max-width="600px">
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
                              <!-- <v-text-field v-model="editedItem.holding" label="Холдинг"></v-text-field> -->
                              <v-select attach v-model="editItem.holding" label="Холдинг"
                               :items="subjects.map(subj => { return subj.holding.name ? subj.holding.name : subj.holding })"></v-select>
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
                <td>{{props.item.id}}</td>
                <td>{{props.item.name}}</td>
                <td>{{props.item.okved}}</td>
                <td>{{props.item.inn}}</td>
                <td>{{props.item.address}}</td>
                <td>{{props.item.postAddress}}</td>
                <td>{{props.item.kpp}}</td>
                <td>{{props.item.holding}}</td>
                <td>{{props.item.email}}</td>
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
      {text: 'Оквед', value: 'okved'},
      {text: 'ИНН', value: 'inn'},
      {text: 'Адрес', value: 'address'},
      {text: 'Почтовый адрес', value: 'postAddress'},
      {text: 'КПП', value: 'kpp'},
      {text: 'Холдинг', value: 'holding'},
      {text: 'E-mail', value: 'email'}
    ],
    subjects: [],
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
      this.subjects = [
        {id: 1, name: 'Субъект1', okved: 'окв', inn: '213211', address: 'ул. КАСА д 1 кожевника', postAddress: 'Такой же', kpp: 'кпп', holding: 'АО Кожевники', email: 'kozhevnik_misos@misos.ru'},
        {id: 2, name: 'Субъект2', okved: 'окв', inn: '213212', address: 'ул. КАСА д 2 кожевника', postAddress: 'Такой же', kpp: 'кпп', holding: 'АО Кожевники', email: 'kozhevnik_misos@misos.ru'},
        {id: 3, name: 'Субъект3', okved: 'окв', inn: '213213', address: 'ул. КАСА д 3кожевника', postAddress: 'Такой же', kpp: 'кпп', holding: 'АО Кожевники', email: 'kozhevnik_misos@misos.ru'},
        {id: 4, name: 'Субъект4', okved: 'окв', inn: '213215', address: 'ул. КАСА д 4кожевника', postAddress: 'Такой же', kpp: 'кпп', holding: 'АО Кожевники', email: 'kozhevnik_misos@misos.ru'}

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
