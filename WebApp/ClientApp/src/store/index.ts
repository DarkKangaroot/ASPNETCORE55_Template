import Vue from 'vue'
import Vuex from 'vuex'
import AppStore from "./modules/App/app.store"
import NotificationStore from "./modules/App/notification.store"

Vue.use(Vuex)

export default new Vuex.Store({
  
  modules: {
      AppStore,
      NotificationStore,
  }
})
