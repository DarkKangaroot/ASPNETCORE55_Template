<template>
    <div>
        <v-app-bar light class="elevation-1" color="white" >
            <v-toolbar-title>{{ systemTitle }}</v-toolbar-title>
        </v-app-bar>

        <v-container>
            <v-card tile width="400">
                <v-card-title class="headline"> Login </v-card-title>
                <v-form ref="loginForm" lazy-validation v-model="loginFormValid">
                   <v-card-text>
                            <v-text-field
                                v-model="loginFormModel.username"
                                name="username"
                                v-on:keyup.enter="submitForm"
                                label="Username"
                                :rules="loginRules.username"
                                autocomplete="off"
                                prepend-icon="mdi-account-circle"
                                required
                                solo
                                clearable
                            ></v-text-field>

                            <v-text-field
                                v-model="loginFormModel.password"
                                v-on:keyup.enter="submitForm"
                                :rules="loginRules.password"
                                :append-icon="showPassword ? 'mdi-eye' : 'mdi-eye-off'"
                                :type="showPassword ? 'text' : 'password'"
                                @click:append="showPassword = !showPassword"
                                name="password"
                                label="Password"
                                prepend-icon="mdi-lock"
                                required
                                clearable
                                solo
                                mb-5
                            ></v-text-field>
                            <!-- <v-select
                                v-model="companySelect"
                                :items="companyList"
                                item-text="companyName"
                                item-value="companyCode"
                                prepend-icon="mdi-database"
                                :label="isCompanyFetching ? `Loading Client..` : `Client`"
                                single-line
                                :loading="isCompanyFetching"
                            ></v-select>    -->
                        </v-card-text>
                        <v-divider></v-divider>
                        <v-card-actions>
                            <v-spacer></v-spacer>
                            <v-btn color="info" @click="submitForm" :loading="loginLoading">Login</v-btn>
                        </v-card-actions>
                </v-form>
            </v-card>
        </v-container>
    </div>
</template>


<script lang="ts">
import { Vue, Component, Ref} from "vue-property-decorator"
import { VForm } from '@/plugins/vuetify'

import { namespace } from 'vuex-class';

const appStore = namespace('AppStore');

@Component({
})
export default class AppBar extends Vue {
    @Ref("loginForm") readonly loginForm!: VForm;

    @appStore.State
    systemTitle!: string

    @appStore.Action
    private userLoginAction!: () => void

    loginFormValid = true
    loginLoading = false;
    showPassword = false;
    loginFormModel = {
        username: "",
        password: ""
    };

    loginRules = {
        username: [
            (v: string): boolean | string => !!v || 'Please input username',
        ],
        password: [
            (v: string): boolean | string => !!v || 'Please input password',
            (v: string): boolean | string => (v && v.length >= 6) || 'Password length should be greater than 6 characters',
        ]
    };

    submitForm(): void {
        if(this.loginForm.validate()){
            this.userLoginAction();
        }
        
    }

    resetForm(): void {
        this.loginForm.resetFields();
    }

}
</script>

<style>
   
  .box-card {
    width: 378px;
    /* resize: both; */
  }
</style>