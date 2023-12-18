<template>
  <div class="container">
    <div class="header">
      <img
        @click="homeOnClick()"
        width="182"
        height="30"
        src="../public/63126f50dbf45.png"
        alt="logo"
      />
      <MSInputSearch
        v-model:value="filter.SearchTerm"
        placeholder="Nhập tên trong giấy tờ, tên đồ vật, tên vật nuôi ..."
        @inputSearchOnSubmit="searchOnClick()"
      ></MSInputSearch>

      <div class="icon-list">
        <!-- Icon đăng bài -->
        <div class="icon-container" @click="addPostOnClick()">
          <svg
            xmlns="http://www.w3.org/2000/svg"
            height="1.5em"
            viewBox="0 0 448 512"
          >
            <path
              d="M64 80c-8.8 0-16 7.2-16 16V416c0 8.8 7.2 16 16 16H384c8.8 0 16-7.2 16-16V96c0-8.8-7.2-16-16-16H64zM0 96C0 60.7 28.7 32 64 32H384c35.3 0 64 28.7 64 64V416c0 35.3-28.7 64-64 64H64c-35.3 0-64-28.7-64-64V96zM200 344V280H136c-13.3 0-24-10.7-24-24s10.7-24 24-24h64V168c0-13.3 10.7-24 24-24s24 10.7 24 24v64h64c13.3 0 24 10.7 24 24s-10.7 24-24 24H248v64c0 13.3-10.7 24-24 24s-24-10.7-24-24z"
            />
          </svg>
        </div>

        <!-- Icon thông báo -->
        <div class="icon-container">
          <svg
            xmlns="http://www.w3.org/2000/svg"
            height="1.5em"
            viewBox="0 0 448 512"
          >
            <path
              d="M224 0c-17.7 0-32 14.3-32 32V49.9C119.5 61.4 64 124.2 64 200v33.4c0 45.4-15.5 89.5-43.8 124.9L5.3 377c-5.8 7.2-6.9 17.1-2.9 25.4S14.8 416 24 416H424c9.2 0 17.6-5.3 21.6-13.6s2.9-18.2-2.9-25.4l-14.9-18.6C399.5 322.9 384 278.8 384 233.4V200c0-75.8-55.5-138.6-128-150.1V32c0-17.7-14.3-32-32-32zm0 96h8c57.4 0 104 46.6 104 104v33.4c0 47.9 13.9 94.6 39.7 134.6H72.3C98.1 328 112 281.3 112 233.4V200c0-57.4 46.6-104 104-104h8zm64 352H224 160c0 17 6.7 33.3 18.7 45.3s28.3 18.7 45.3 18.7s33.3-6.7 45.3-18.7s18.7-28.3 18.7-45.3z"
            />
          </svg>
        </div>

        <!-- Icon user -->
        <div v-click-outside="userPopupClickOutSide" class="relative">
          <div class="icon-container" @click="iconUserOnClick()">
            <svg
              xmlns="http://www.w3.org/2000/svg"
              height="1.5em"
              viewBox="0 0 448 512"
            >
              <path
                d="M304 128a80 80 0 1 0 -160 0 80 80 0 1 0 160 0zM96 128a128 128 0 1 1 256 0A128 128 0 1 1 96 128zM49.3 464H398.7c-8.9-63.3-63.3-112-129-112H178.3c-65.7 0-120.1 48.7-129 112zM0 482.3C0 383.8 79.8 304 178.3 304h91.4C368.2 304 448 383.8 448 482.3c0 16.4-13.3 29.7-29.7 29.7H29.7C13.3 512 0 498.7 0 482.3z"
              />
            </svg>
          </div>
          <div v-show="isShowUserPopup" class="function-container">
            <div v-if="isLoggedIn() == true" class="logged-in-functions">
              <div class="func-item" @click="showProfileOnClick()">
                <svg
                  xmlns="http://www.w3.org/2000/svg"
                  height="1.25em"
                  viewBox="0 0 576 512"
                >
                  <!--! Font Awesome Free 6.4.2 by @fontawesome - https://fontawesome.com License - https://fontawesome.com/license (Commercial License) Copyright 2023 Fonticons, Inc. -->
                  <path
                    d="M575.8 255.5c0 18-15 32.1-32 32.1h-32l.7 160.2c0 2.7-.2 5.4-.5 8.1V472c0 22.1-17.9 40-40 40H456c-1.1 0-2.2 0-3.3-.1c-1.4 .1-2.8 .1-4.2 .1H416 392c-22.1 0-40-17.9-40-40V448 384c0-17.7-14.3-32-32-32H256c-17.7 0-32 14.3-32 32v64 24c0 22.1-17.9 40-40 40H160 128.1c-1.5 0-3-.1-4.5-.2c-1.2 .1-2.4 .2-3.6 .2H104c-22.1 0-40-17.9-40-40V360c0-.9 0-1.9 .1-2.8V287.6H32c-18 0-32-14-32-32.1c0-9 3-17 10-24L266.4 8c7-7 15-8 22-8s15 2 21 7L564.8 231.5c8 7 12 15 11 24z"
                  />
                </svg>
                <div class="func-name">Trang cá nhân</div>
              </div>

              <div class="func-item" @click="editProfileOnClick()">
                <svg
                  xmlns="http://www.w3.org/2000/svg"
                  height="1.25em"
                  viewBox="0 0 512 512"
                >
                  <!--! Font Awesome Free 6.4.2 by @fontawesome - https://fontawesome.com License - https://fontawesome.com/license (Commercial License) Copyright 2023 Fonticons, Inc. -->
                  <path
                    d="M441 58.9L453.1 71c9.4 9.4 9.4 24.6 0 33.9L424 134.1 377.9 88 407 58.9c9.4-9.4 24.6-9.4 33.9 0zM209.8 256.2L344 121.9 390.1 168 255.8 302.2c-2.9 2.9-6.5 5-10.4 6.1l-58.5 16.7 16.7-58.5c1.1-3.9 3.2-7.5 6.1-10.4zM373.1 25L175.8 222.2c-8.7 8.7-15 19.4-18.3 31.1l-28.6 100c-2.4 8.4-.1 17.4 6.1 23.6s15.2 8.5 23.6 6.1l100-28.6c11.8-3.4 22.5-9.7 31.1-18.3L487 138.9c28.1-28.1 28.1-73.7 0-101.8L474.9 25C446.8-3.1 401.2-3.1 373.1 25zM88 64C39.4 64 0 103.4 0 152V424c0 48.6 39.4 88 88 88H360c48.6 0 88-39.4 88-88V312c0-13.3-10.7-24-24-24s-24 10.7-24 24V424c0 22.1-17.9 40-40 40H88c-22.1 0-40-17.9-40-40V152c0-22.1 17.9-40 40-40H200c13.3 0 24-10.7 24-24s-10.7-24-24-24H88z"
                  />
                </svg>
                <div class="func-name">Cập nhật thông tin</div>
              </div>

              <div class="func-item" @click="resetPasswordOnClick()">
                <svg
                  xmlns="http://www.w3.org/2000/svg"
                  height="1.25em"
                  viewBox="0 0 512 512"
                >
                  <!--! Font Awesome Free 6.4.2 by @fontawesome - https://fontawesome.com License - https://fontawesome.com/license (Commercial License) Copyright 2023 Fonticons, Inc. -->
                  <path
                    d="M336 352c97.2 0 176-78.8 176-176S433.2 0 336 0S160 78.8 160 176c0 18.7 2.9 36.8 8.3 53.7L7 391c-4.5 4.5-7 10.6-7 17v80c0 13.3 10.7 24 24 24h80c13.3 0 24-10.7 24-24V448h40c13.3 0 24-10.7 24-24V384h40c6.4 0 12.5-2.5 17-7l33.3-33.3c16.9 5.4 35 8.3 53.7 8.3zM376 96a40 40 0 1 1 0 80 40 40 0 1 1 0-80z"
                  />
                </svg>
                <div class="func-name">Đổi mật khẩu</div>
              </div>
              <hr />
              <div class="func-item" @click="signOutOnClick()">
                <svg
                  xmlns="http://www.w3.org/2000/svg"
                  height="1.25em"
                  viewBox="0 0 512 512"
                  style="fill: #d24646"
                >
                  <!--! Font Awesome Free 6.4.2 by @fontawesome - https://fontawesome.com License - https://fontawesome.com/license (Commercial License) Copyright 2023 Fonticons, Inc. -->
                  <path
                    d="M502.6 278.6c12.5-12.5 12.5-32.8 0-45.3l-128-128c-12.5-12.5-32.8-12.5-45.3 0s-12.5 32.8 0 45.3L402.7 224 192 224c-17.7 0-32 14.3-32 32s14.3 32 32 32l210.7 0-73.4 73.4c-12.5 12.5-12.5 32.8 0 45.3s32.8 12.5 45.3 0l128-128zM160 96c17.7 0 32-14.3 32-32s-14.3-32-32-32L96 32C43 32 0 75 0 128L0 384c0 53 43 96 96 96l64 0c17.7 0 32-14.3 32-32s-14.3-32-32-32l-64 0c-17.7 0-32-14.3-32-32l0-256c0-17.7 14.3-32 32-32l64 0z"
                  />
                </svg>
                <div class="func-name">Đăng xuất</div>
              </div>
            </div>

            <div v-else class="logged-out-functions">
              <div class="func-item" @click="signInOnClick()">
                <svg
                  xmlns="http://www.w3.org/2000/svg"
                  height="1.25em"
                  viewBox="0 0 512 512"
                >
                  <!--! Font Awesome Free 6.4.2 by @fontawesome - https://fontawesome.com License - https://fontawesome.com/license (Commercial License) Copyright 2023 Fonticons, Inc. -->
                  <path
                    d="M217.9 105.9L340.7 228.7c7.2 7.2 11.3 17.1 11.3 27.3s-4.1 20.1-11.3 27.3L217.9 406.1c-6.4 6.4-15 9.9-24 9.9c-18.7 0-33.9-15.2-33.9-33.9l0-62.1L32 320c-17.7 0-32-14.3-32-32l0-64c0-17.7 14.3-32 32-32l128 0 0-62.1c0-18.7 15.2-33.9 33.9-33.9c9 0 17.6 3.6 24 9.9zM352 416l64 0c17.7 0 32-14.3 32-32l0-256c0-17.7-14.3-32-32-32l-64 0c-17.7 0-32-14.3-32-32s14.3-32 32-32l64 0c53 0 96 43 96 96l0 256c0 53-43 96-96 96l-64 0c-17.7 0-32-14.3-32-32s14.3-32 32-32z"
                  />
                </svg>
                <div class="func-name">Đăng nhập</div>
              </div>
              <div class="func-item" @click="signUpOnClick()">
                <svg
                  xmlns="http://www.w3.org/2000/svg"
                  height="1.25em"
                  viewBox="0 0 512 512"
                >
                  <!--! Font Awesome Free 6.4.2 by @fontawesome - https://fontawesome.com License - https://fontawesome.com/license (Commercial License) Copyright 2023 Fonticons, Inc. -->
                  <path
                    d="M441 58.9L453.1 71c9.4 9.4 9.4 24.6 0 33.9L424 134.1 377.9 88 407 58.9c9.4-9.4 24.6-9.4 33.9 0zM209.8 256.2L344 121.9 390.1 168 255.8 302.2c-2.9 2.9-6.5 5-10.4 6.1l-58.5 16.7 16.7-58.5c1.1-3.9 3.2-7.5 6.1-10.4zM373.1 25L175.8 222.2c-8.7 8.7-15 19.4-18.3 31.1l-28.6 100c-2.4 8.4-.1 17.4 6.1 23.6s15.2 8.5 23.6 6.1l100-28.6c11.8-3.4 22.5-9.7 31.1-18.3L487 138.9c28.1-28.1 28.1-73.7 0-101.8L474.9 25C446.8-3.1 401.2-3.1 373.1 25zM88 64C39.4 64 0 103.4 0 152V424c0 48.6 39.4 88 88 88H360c48.6 0 88-39.4 88-88V312c0-13.3-10.7-24-24-24s-24 10.7-24 24V424c0 22.1-17.9 40-40 40H88c-22.1 0-40-17.9-40-40V152c0-22.1 17.9-40 40-40H200c13.3 0 24-10.7 24-24s-10.7-24-24-24H88z"
                  />
                </svg>
                <div class="func-name">Đăng ký</div>
              </div>
            </div>
          </div>
        </div>
      </div>
    </div>
    <div class="body">
      <div v-if="navType == 0" class="nav">
        <div class="nav__head">
          <div class="nav__row">
            <div class="nav__title">Bộ lọc</div>
            <div class="delete-filter flex" @click="resetFilter()">
              <svg
                xmlns="http://www.w3.org/2000/svg"
                height="1.25em"
                viewBox="0 0 448 512"
              >
                <path
                  d="M135.2 17.7L128 32H32C14.3 32 0 46.3 0 64S14.3 96 32 96H416c17.7 0 32-14.3 32-32s-14.3-32-32-32H320l-7.2-14.3C307.4 6.8 296.3 0 284.2 0H163.8c-12.1 0-23.2 6.8-28.6 17.7zM416 128H32L53.2 467c1.6 25.3 22.6 45 47.9 45H346.9c25.3 0 46.3-19.7 47.9-45L416 128z"
                />
              </svg>
              Xóa bộ lọc
            </div>
          </div>
          <hr />
          <div class="nav__row">
            <MSInputRadio
              title="Loại tin"
              direction="column"
              :items="[
                { id: 0, value: 'Tin cần tìm' },
                { id: 1, value: 'Tin nhặt được' },
              ]"
              v-model:id="filter.NewsType"
            ></MSInputRadio>
          </div>
          <div class="nav__row">
            <MSInputRadio
              title="Danh mục"
              direction="column"
              :items="[
                { id: 0, value: 'Ví/Giấy tờ' },
                { id: 1, value: 'Thú cưng(Chó/Mèo)' },
                { id: 2, value: 'Tìm người' },
                { id: 3, value: 'Điện thoại/Tablet/Laptop' },
                { id: 4, value: 'Chìa khóa' },
                { id: 5, value: 'Xe máy/Ô tô' },
                { id: 6, value: 'Đồ vật khác' },
              ]"
              v-model:id="filter.Category"
            ></MSInputRadio>
          </div>
          <div class="nav__row">
            <MSInputCombobox
              title="Chọn khu vục"
              propText="Name"
              propValue="CityId"
              v-model:id="filter.ProvinceId"
              :listItems="provinces"
            >
            </MSInputCombobox>
          </div>
          <div class="nav__row">
            <button class="main-button" @click="searchOnClick()">
              Tìm kiếm
            </button>
          </div>
        </div>
      </div>
      <div class="content">
        <router-view :key="postListKey"></router-view>
      </div>
    </div>
  </div>
  <MSReminderNotice></MSReminderNotice>
  <MSTooltip></MSTooltip>
</template>

<script>
import {
  getAuth,
  onAuthStateChanged,
  signOut,
  sendPasswordResetEmail,
} from "firebase/auth";

export default {
  name: "App",
  components: {},
  data() {
    return {
      postListKey: 0,
      provinces: [{ CityId: "", Name: "Toàn quốc" }],
      filter: {
        Category: null,
        NewsType: null,
        ProvinceId: "",
        SearchTerm: "",
      },
      auth: null,
      isShowUserPopup: false,
    };
  },
  watch: {},

  computed: {
    navType() {
      if (this.$route.path.startsWith("/TPostDetail")) return 1;
      switch (this.$route.path) {
        case "/TProfile":
          return 1;

        case "/TProfile/edit":
          return 1;

        case "/TPostAdd":
          return 2;

        case "/TSignIn":
          return 3;

        case "/AdminPostList":
          return 3;

        case "/TSignUp":
          return 4;

        case "/":
          return 0;

        default:
          return 0;
      }
    },
  },

  async created() {
    const response = await this.$msAxios("get", this.$msApi.CityApi.Paging, {
      params: {
        // Kích thước của trang
        pageSize: 100,
        // vị trí trang
        pageNumber: 1,
      },
    });
    for (const key in response.data.Data) {
      const element = response.data.Data[key];
      this.provinces.push(element);
    }
  },

  mounted() {
    this.auth = getAuth();

    onAuthStateChanged(this.auth, () => {
      console.log("Đã đăng nhập: ", this.isLoggedIn());
    });
  },

  beforeUnmount() {},

  methods: {
    resetPasswordOnClick() {
      if (!this.auth?.currentUser?.email) return;
      sendPasswordResetEmail(this.auth, this.auth?.currentUser?.email)
        .then(() => {
          this.$msEmitter.emit("addNotice", {
            type: this.$msEnum.NoticeType.Success,
            message: "Hãy kiểm tra gmail của bạn",
          });
        })
        .catch(() => {
          this.$msEmitter.emit("addNotice", {
            type: this.$msEnum.NoticeType.Error,
            message: "Gửi gmail thất bại",
          });
        });
      this.isShowUserPopup = false;
    },
    userPopupClickOutSide() {
      this.isShowUserPopup = false;
    },
    isLoggedIn() {
      this.auth = getAuth();
      if (!this.auth?.currentUser?.accessToken) {
        return this.$msCookies.get("accessToken");
      } else {
        this.$msCookies.set("accessToken", this.auth.currentUser.accessToken);
      }
      return true;
    },

    signOutOnClick() {
      signOut(this.auth).then(() => {
        this.$router.push("/");
        this.$msCookies.set("accessToken", "");
      });

      this.isShowUserPopup = false;
    },
    async searchOnClick() {
      await this.$router.replace(
        "/" + encodeURIComponent(JSON.stringify(this.filter))
      );
      this.reloadListPost();
    },
    resetFilter() {
      this.filter = {
        Category: null,
        NewsType: null,
        ProvinceId: 0,
      };
    },
    reloadListPost() {
      this.postListKey += 1;
    },
    addPostOnClick() {
      this.$router.push("/TPostAdd");
    },
    signInOnClick() {
      this.$router.push("/TSignIn");
      this.isShowUserPopup = false;
    },

    signUpOnClick() {
      this.$router.push("/TSignUp");
      this.isShowUserPopup = false;
    },

    homeOnClick() {
      this.$router.push("/");
    },

    iconUserOnClick() {
      this.isShowUserPopup = !this.isShowUserPopup;
    },
    showProfileOnClick() {
      this.$router.push("/TProfile");
      this.isShowUserPopup = false;
    },
    editProfileOnClick() {
      this.$router.push("/TProfile/edit");
      this.isShowUserPopup = false;
    },
  },
};
</script>

<style lang="scss">
.container {
  .header {
    img {
      cursor: pointer;
    }

    width: 100vw;
    height: 50px;
    background-color: white;
    box-shadow: 0 2px 2px rgba(0, 0, 0, 0.1);

    display: flex;
    flex-direction: rows;
    align-items: center;
    justify-content: space-around;
    .input-search {
      width: 600px;
      position: relative;
      top: 4px;
    }

    .icon-list {
      display: flex;

      .relative {
        position: relative;
      }

      .icon-container {
        width: 40px;
        height: 40px;
        margin: 0px 12px;
        border-radius: var(--border-radius);
        background-color: #fff;
        transition: background-color 0.5s ease;

        &:hover {
          background-color: #d9d9d9;
        }
      }

      .function-container {
        z-index: 99;
        box-shadow: 0 2px 2px rgba(0, 0, 0, 0.1);
        background-color: #fff;
        border: 1px solid #d9d9d9;
        border-radius: 4px;
        position: absolute;
        top: 40px;
        left: -125px;
        user-select: none;

        .logged-in-functions {
          width: 175px;
          display: flex;
          flex-direction: column;
          padding: 4px;

          .func-item {
            cursor: pointer;
            display: flex;
            padding: 5px;
            align-items: center;
            column-gap: 5px;
            border-radius: 4px;

            &:hover {
              background-color: var(--color-light-green-200);
            }

            &:last-child {
              color: #dc3545;
            }

            .icon-container {
              margin: 0px;
              width: 20px;
              height: 20px;
            }
          }
        }

        .logged-out-functions {
          width: 175px;
          display: flex;
          flex-direction: column;
          padding: 4px;

          .func-item {
            cursor: pointer;
            display: flex;
            padding: 5px;
            align-items: center;
            column-gap: 5px;
            border-radius: 4px;

            &:hover {
              background-color: var(--color-light-green-200);
            }

            .icon-container {
              margin: 0px;
              width: 20px;
              height: 20px;
            }
          }
        }
      }
    }
  }

  .body {
    display: flex;
    background-color: #f4f6f9;
    .nav {
      padding: 24px;
      width: 300px;
      height: calc(100vh - 50px);
      margin-left: 35px;

      .nav__head {
        user-select: none;
        .nav__title {
          font-weight: bold;
        }
        .nav__row {
          display: flex;
          label {
            font-weight: bold;
            height: 40px;
          }

          label:has(input[type="radio"]) {
            font-weight: 400;
          }

          &:nth-child(1) {
            align-items: center;
            padding: 4px 0px;
            justify-content: space-between;
            svg {
              fill: red;
              margin-right: 4px;
            }
            .delete-filter {
              align-items: center;
              cursor: pointer;
              color: #ff0000;
            }
          }

          &:nth-child(3) {
            margin-top: 10px;
          }
          &:nth-child(4) {
            margin-top: 10px;
          }
          &:nth-child(5) {
            margin-top: 10px;
          }

          &:nth-child(6) {
            justify-content: center;
          }

          .mcombobox {
            margin-top: 8px;
          }
        }
      }
    }
    &:has(.nav) {
      .content {
        width: calc(100vw - 300px);
      }
    }
    .content {
      height: calc(100vh - 50px);
      width: 100vw;
      overflow: auto;

      &::-webkit-scrollbar {
        width: 5px;
        height: 5px;
      }

      /* Handle */
      &::-webkit-scrollbar-thumb {
        background: #ccc;
      }

      /* Handle on hover */
      &::-webkit-scrollbar-thumb:hover {
        background: #a7a7a7;
      }
    }
  }
}
</style>

<style src="./style/base/main.scss" lang="scss">
</style>
