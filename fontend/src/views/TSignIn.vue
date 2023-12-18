<template>
  <div class="center">
    <div class="sign-in__form">
      <div class="head">
        <h1>Đăng nhập</h1>
        <div class="icon-container" @click="goHomeOnClick()">
          <div class="icon-x-black"></div>
        </div>
      </div>
      <div class="body">
        <div class="flex center img">
          <img
            width="182"
            height="30"
            src="../../public/63126f50dbf45.png"
            alt="logo"
          />
        </div>
        <MSInputText
          :isValidating="isValidating"
          :errorMessage="emailMessage"
          inputType="email"
          title="Email"
          isRequired
          v-model:value="account.email"
          :onDoneTyping="checkEmail"
        ></MSInputText>
        <MSInputText
          :isValidating="isValidating"
          :errorMessage="passwordMessage"
          inputType="password"
          title="Mật khẩu"
          isRequired
          v-model:value="account.pass"
        ></MSInputText>
        <div class="login">
          <button class="main-button" @click="login()">Đăng nhập</button>
        </div>
      </div>
      <div class="foot">
        <div class="foot__head">
          <div class="tit">Hoặc</div>
        </div>
        <div class="login-option">
          <button class="extra-button" @click="signUpOnClick()">Đăng ký</button>
          <button class="extra-button" @click="signInWithGoogle()">
            Đăng nhập bằng Google
          </button>
        </div>
      </div>
    </div>
  </div>
</template>
  
  <script>
import {
  getAuth,
  GoogleAuthProvider,
  signInWithPopup,
  signInWithEmailAndPassword,
} from "firebase/auth";

export default {
  name: "TSignIn",
  components: {},
  props: [],
  data() {
    return {
      isValidating: false,
      emailMessage: "Email không được để trống",
      passwordMessage: "Mật khẩu không được để trống",
      account: {
        pass: "",
        email: "",
      },
    };
  },
  created() {},
  methods: {
    goHomeOnClick() {
      this.$router.push("/");
    },
    validateLogin() {
      this.isValidating = true;

      const notEmpty = this.account.email && this.account.pass;
      const isEmail = !this.checkEmail().isErrored;

      const isValid = notEmpty && isEmail;
      if (isValid) {
        this.isValidating = false;
      }
      console.log(isValid);
      return isValid;
    },
    checkEmail() {
      if (!this.account.email) {
        return {
          isErrored: true,
          message: "Email không được để trống",
        };
      }
      const regex = /[^\s@]+@[^\s@]+\.[^\s@]+/;
      const isEmail = this.account.email.match(regex);
      if (!isEmail)
        return {
          isErrored: true,
          message: "Email không đúng định dạng",
        };
      return {
        isErrored: false,
        message: "",
      };
    },
    checkStrongPass() {
      if (!this.account?.pass || this.account?.pass?.length < 8) {
        return {
          isErrored: true,
          message: "Mật khẩu phải có ít nhất 8 ký tự",
        };
      }

      var pattern = /(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*\W)/;
      if (!pattern.test(this.account?.pass)) {
        return {
          isErrored: true,
          message:
            "Mật khẩu phải chứa ít nhất một ký tự chữ hoa, chữ thường, số và ký tự đặc biệt.",
        };
      }
      return {
        isErrored: false,
        message: "",
      };
    },
    async login() {
      if (!this.validateLogin()) return;
      const auth = getAuth();

      try {
        const resData = await signInWithEmailAndPassword(
          auth,
          this.account.email,
          this.account.pass
        );
        if (resData?.user?.accessToken) {
          this.$msCookies.set("accessToken", resData.user.accessToken);
          const actoken = this.$msCookies.get("accessToken");
          console.log("Success dang nhap", actoken);
        }
        this.$router.push("/");
      } catch (e) {
        console.log("Error", e);
      }
    },
    signInWithGoogle() {
      const provider = new GoogleAuthProvider();
      const auth = getAuth();

      signInWithPopup(auth, provider)
        .then((result) => {
          console.log("Success dang nhap", result);

          if (result?.user?.accessToken) {
            this.$msCookies.set("accessToken", result.user.accessToken);
            // const actoken = this.$msCookies.get("accessToken");
            // console.log("Success dang nhap", actoken);
          }

          if (result?.user?.photoURL) {
            this.$msCookies.set("photoURL", result.user.photoURL);
            console.log(this.$msCookies.get("photoURL"));
          }

          this.$router.push("/");
        })
        .catch((error) => {
          console.log(error);
        });
    },
    signUpOnClick() {
      this.$router.push("/TSignUp");
    },
  },
};
</script>
  
  <style lang="scss">
.center {
  height: 100%;
}
.sign-in__form {
  width: 600px;
  height: 700px;
  background-color: #fff;
  border-radius: 5px;
  .head {
    h1 {
      font-size: 24px;
    }
    display: flex;
    align-items: center;
    justify-content: space-between;
    padding: 12px;
    border-bottom: 1px solid #ccc;
  }
  .body {
    display: flex;
    background-color: #fff;
    flex-direction: column;
    padding: 12px;
    gap: 12px;
    margin-top: 20px;

    .img {
      margin: 30px 0px;
    }

    .input {
      * {
        font-size: 14px !important;
      }
      .input__label {
        padding-bottom: 6px;
      }
      &:nth-child(1) {
        margin: 0px;
      }

      &:nth-child(2) {
        margin: 0px;
      }
    }
    .login {
      margin-top: 30px;
      display: flex;
      justify-content: center;
      flex-direction: row;
      button {
        width: 100%;
      }
    }
  }
  .foot {
    display: flex;
    flex-direction: column;
    justify-content: center;

    .foot__head {
      .tit {
        text-align: center;
        justify-content: center !important;
        font-size: 14px;
      }
      padding-bottom: 12px;
      margin: 30px 0px;
    }
    .login-option {
      display: flex;
      justify-content: space-around;
      padding: 0px 12px;
      gap: 12px;
      button {
        width: 100%;
      }
    }
  }
}
</style>