

import {
    VuexModule
    , Module
    , Mutation
    , Action
}
from 'vuex-module-decorators'
// import jwtToken from "@/services/jwt.token"
// import LoginDto from '@/interface/User/Login.dto';

import tempRoutes from "@/store/tempData/tempRoutes"


@Module({ namespaced: true })
class AppStore extends VuexModule {
    //State
    routes = tempRoutes;
    isLogin = true;
    systemTitle = process.env.VUE_APP_NAME

    //Mutation
    @Mutation
    checkIsLogin(): void {
        // this.isLogin = jwtToken.checkToken();
        // this.systemTitle = jwtToken.currentSystemTitle;
    }

    @Mutation
    // eslint-disable-next-line @typescript-eslint/no-unused-vars
    // userlogin(dto: LoginDto): void {
    //     //TODO: login request
    //     this.isLogin = true;
    // }

    @Mutation
    userlogout(): void {
        // others need to destroy
        this.isLogin = false;
    }

    //Action
    @Action({rawError: true})
    checkIsLoginAction(): void{
        this.context.commit('checkIsLogin');
    }

    // @Action({rawError: true})
    // userLoginAction(dto: LoginDto): void{
    //     this.context.commit('userlogin', dto);
    // }

    @Action({rawError: true})
    userLogoutAction(): void{
        this.context.commit('userlogout');
    }
}

export default AppStore