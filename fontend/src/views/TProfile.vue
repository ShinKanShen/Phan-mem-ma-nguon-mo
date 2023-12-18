<template>
  <div class="t-profile">
    <div class="profile__head">
      <div class="t-title">Thông tin tài khoản</div>
    </div>

    <div class="profile__body">
      <MSInputText
        v-model:value="user.FullName"
        :readonly="isReadonly"
        title="Họ và tên"
      ></MSInputText>

      <MSInputText
        v-model:value="user.PhoneNumber"
        :readonly="isReadonly"
        title="Số điện thoại"
      ></MSInputText>

      <MSInputRadio
        :readonly="isReadonly"
        title="Giới tính"
        direction="row"
        :items="[
          { id: 0, value: 'Nam' },
          { id: 1, value: 'Nữ' },
        ]"
        v-model:id="user.Gender"
      ></MSInputRadio>

      <MSInputText
        v-model:value="user.YearOfBirth"
        :readonly="isReadonly"
        title="Năm sinh"
      ></MSInputText>

      <MSInputText
        v-model:value="user.Address"
        :readonly="isReadonly"
        title="Địa chỉ"
      ></MSInputText>
    </div>
    <div class="profile__foot">
      <div v-if="isReadonly" @click="editOnClick()" class="main-button">
        Chỉnh sửa
      </div>

      <div v-else class="main-button" @click="saveOnClick()">Lưu</div>
    </div>
  </div>
</template>

<script>
export default {
  name: "TProfile",
  components: {},
  props: {
    readonly: {
      type: String,
      default: "",
    },
  },
  data() {
    return {
      header: {
        headers: {
          Authorization: "Bearer ",
        },
      },
      user: {},
      auth: null,
      isReadonly: !this.readonly,
    };
  },
  watch: {
    readonly() {
      this.isReadonly = !this.readonly;
    },
  },

  computed: {},

  async created() {
    const cookie = this.$msCookies.get("accessToken");
    this.header = {
      headers: {
        Authorization: "Bearer " + cookie,
      },
    };

    const response = await this.$msAxios(
      "get",
      this.$msApi.AccountApi.CheckToken,
      this.header
    );
    this.user = response.data;
    console.log(this.user);
  },

  mounted() {
    const cookie = this.$msCookies.get("accessToken");
    this.header = {
      headers: {
        Authorization: "Bearer " + cookie,
      },
    };

    if (cookie == false) {
      this.$router.push("/");
    }
  },

  beforeUnmount() {},

  methods: {
    async saveOnClick() {
      const response = await this.$msAxios(
        "put",
        this.$msApi.AccountApi.Put(this.user.AccountId),
        this.user,
        this.header
      );

      if (response.status == 200) {
        this.$msEmitter.emit("addNotice", {
          type: this.$msEnum.NoticeType.Success,
          message: "Sửa thành công",
        });
        this.$router.push("/");
      } else {
        this.$msEmitter.emit("addNotice", {
          type: this.$msEnum.NoticeType.Error,
          message: "Có lỗi xảy ra",
        });
      }
    },
    editOnClick() {
      this.isReadonly = false;
    },
  },
};
</script>

<style lang="scss">
.t-profile {
  position: fixed;
  left: calc(50vw - 300px);
  margin-top: 24px;
  background-color: #fff;
  width: 600px;
  padding: 24px;

  .profile__head {
    .t-title {
      font-size: 16px;
      font-weight: bold;
    }
    padding-bottom: 12px;
  }

  .profile__body {
    display: flex;
    flex-direction: column;
    row-gap: 12px;

    .input {
      .input__label {
        font-size: 14px !important;
        padding-bottom: 6px;
      }
    }
    padding-bottom: 12px;
  }

  .profile__foot {
    display: flex;
    justify-content: flex-end;
  }
}
</style>