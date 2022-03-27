<template>
  <v-app>
    <Notification />
    <ConfirmDialog />
    <Login v-if="!isLogin"/>
    <div v-else>
      <v-app-bar app dark class="elevation-0 purple darken-2" color="white" clipped-left>
          <v-app-bar-nav-icon @click="mini = !mini"></v-app-bar-nav-icon>
          <v-toolbar-title>{{ systemTitle }}</v-toolbar-title>

          <v-spacer></v-spacer>
          <v-menu left bottom >
            <template v-slot:activator="{ on, attrs }">
              <v-btn icon v-bind="attrs" v-on="on">
                <v-icon>mdi-dots-vertical</v-icon>
              </v-btn>
            </template>

            <v-list>
              <v-list-item>
                <v-list-item-title>Hi, </v-list-item-title>
              </v-list-item>
              <v-list-item @click="() => {}">
                <v-list-item-title>
                  Logout
                </v-list-item-title>
              </v-list-item>
            </v-list>
          </v-menu>
      </v-app-bar>

      <v-navigation-drawer 
      
        width="250"
        class="purple darken-2"
      
        permanent
        fixed
        app
        bottom
        clipped
        dark
        :mini-variant="mini"
        :expand-on-hover="mini"
      >
      <v-list dense nav>
        <div v-for="(item, key) in routes" :key="key">
          <div v-if="item.subRoute.length == 0">
            <v-list-item link :to="item.route">
              <v-list-item-icon>
                <v-icon>{{ item.icon }}</v-icon>
              </v-list-item-icon>

              <v-list-item-content>
                <v-list-item-title>{{ item.title }}</v-list-item-title>
              </v-list-item-content>
            </v-list-item>
          </div>
          <div v-else>
            <v-list-group :prepend-icon="item.icon" no-action>
              <template v-slot:activator>
                <v-list-item-title>{{ item.title }}</v-list-item-title>
              </template>

              <v-list-item v-for="(sub, subKey) in item.subRoute" :key="subKey" link :to="sub.route">
                <v-list-item-icon>
                  <v-icon>{{ sub.icon }}</v-icon>
                </v-list-item-icon>

                <v-list-item-content>
                  <v-list-item-title>{{ sub.title }}</v-list-item-title>
                </v-list-item-content>
              </v-list-item>
            </v-list-group>
          </div>
        </div>
      </v-list>
      </v-navigation-drawer>

      <!-- Sizes your content based upon application components -->
      <v-main>
        <v-container fluid pa-6>
          <router-view></router-view>
        </v-container>
      </v-main>

      <!-- <v-footer app>
      </v-footer> -->
    </div>
    
  </v-app>
</template>

<script lang="ts">

import { Component, Watch, Vue } from "vue-property-decorator"
import { namespace } from 'vuex-class';

const appStore = namespace('AppStore');
@Component({
  components:{
    Login: () => import(`@/components/App/Login.vue`),
    Notification: () => import(`@/components/Notification.vue`),
    ConfirmDialog: () => import(`@/components/ConfirmDialog.vue`),
  }
})
export default class App extends Vue {

    @appStore.State
    systemTitle!: string

    @appStore.State
    isLogin!: boolean
    
    @appStore.State
    routes!: [];

    mini = false;

    @Watch("$vuetify.breakpoint.name", { immediate: true, deep: true })
    watch_mini(val: string): boolean {
      let mini = false;
      switch (val) {
          case 'xs': {
            mini = true;
            break;
          }
          case 'sm': {
            mini = false;
            break;
          }
          case 'md': {
            mini = false;
            break;
          }
          case 'lg': {
            mini = false;
            break;
          }
          case 'xl': {
            mini = false;
            break;
          }
      }
      this.mini = mini;
      return mini
    }
    
    @appStore.Action
    userLogoutAction!: () => void

    @appStore.Action
    miniSideDrawerAction!: (isMini: boolean) => void

    @appStore.Action
    checkIsLoginAction!: () => void

    mounted(): void {
      this.checkIsLoginAction(); 
    }
}

</script>