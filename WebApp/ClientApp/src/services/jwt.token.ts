

import jwt_decode, { JwtPayload} from "jwt-decode";
/*eslint-disable */
class jwtToken {

    checkToken() : boolean {
        const token = localStorage.getItem("token");
        let decodedToken: JwtPayload
        if(token){
            decodedToken = jwt_decode(token);
            
            if(decodedToken.exp){
                if(decodedToken.exp < new Date().getTime() / 1000){
                    return false;
                }
            }
            return true; 
        }

        return false;
    }

    
    async beforeEnter(to: any, from: any, next: any, root: any) : Promise<void> { 
        // const {path} = to;
        if(from.name == "Login" || to.name == "Login"){
            next();
            return;
        }
        if(this.checkToken()){
            next();
        }else{
            localStorage.removeItem("company");
            localStorage.removeItem("menuList");
            localStorage.removeItem("user");
            // const res = await root.confirm(`Session Expire`, "Session Expire Please re-login your account", { noconfirm: true });
            // if(res){
            //     next({
            //         path: "/Login",
            //         query: {
            //             nextUrl: path
            //         }
            //     });
            // }else{
            //     next();
            // }
        }
    }
    
    get currentUser(): string {
        const user = localStorage.getItem("user");
        if(user){
            return user;
        }
        return "";
    }

    get currentEmpCode(): string {
        const empCode = localStorage.getItem("empCode");
        if(empCode){
            return empCode;
        }
        return "";
    }

    get currentSystemTitle(): string {
        const title = localStorage.getItem("systemTitle");
        if(title){
            return title;
        }
        return process.env.VUE_APP_NAME;
    }
}

export default new jwtToken();