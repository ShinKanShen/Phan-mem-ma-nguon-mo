<template>
  <div class="center-container">
    <div class="post">
      <div class="post__head">
        <div class="left flex cg-12">
          <div class="user__img">
            <img width="36" height="36" :src="news.AccountPicture" alt="" />
          </div>
          <div class="block flex-column flex rg-6">
            <div class="user_name">{{ news.AccountName }}</div>
            <div class="post__time">{{ news.TimeAgo }}</div>
          </div>
        </div>
        <div class="right">
          <div class="icon-container" @click="homeOnClick()">
            <div class="icon-x-black"></div>
          </div>
        </div>
      </div>
      <div class="post__body">
        <div class="post__infor">
          <div class="line-1 cg-6 flex">
            <svg
              v-if="true"
              xmlns="http://www.w3.org/2000/svg"
              height="1.25em"
              viewBox="0 0 512 512"
            >
              <path
                d="M400 406.1V288c0-13.3-10.7-24-24-24s-24 10.7-24 24V440.6c-28.7 15-61.4 23.4-96 23.4s-67.3-8.5-96-23.4V288c0-13.3-10.7-24-24-24s-24 10.7-24 24V406.1C72.6 368.2 48 315 48 256C48 141.1 141.1 48 256 48s208 93.1 208 208c0 59-24.6 112.2-64 150.1zM256 512A256 256 0 1 0 256 0a256 256 0 1 0 0 512zM159.6 220c10.6 0 19.9 3.8 25.4 9.7c7.6 8.1 20.2 8.5 28.3 .9s8.5-20.2 .9-28.3C199.7 186.8 179 180 159.6 180s-40.1 6.8-54.6 22.3c-7.6 8.1-7.1 20.7 .9 28.3s20.7 7.1 28.3-.9c5.5-5.8 14.8-9.7 25.4-9.7zm166.6 9.7c5.5-5.8 14.8-9.7 25.4-9.7s19.9 3.8 25.4 9.7c7.6 8.1 20.2 8.5 28.3 .9s8.5-20.2 .9-28.3C391.7 186.8 371 180 351.6 180s-40.1 6.8-54.6 22.3c-7.6 8.1-7.1 20.7 .9 28.3s20.7 7.1 28.3-.9zM208 320v32c0 26.5 21.5 48 48 48s48-21.5 48-48V320c0-26.5-21.5-48-48-48s-48 21.5-48 48z"
              />
            </svg>
            <svg
              v-else
              xmlns="http://www.w3.org/2000/svg"
              height="1.25em"
              viewBox="0 0 512 512"
            >
              <path
                d="M464 256A208 208 0 1 0 48 256a208 208 0 1 0 416 0zM0 256a256 256 0 1 1 512 0A256 256 0 1 1 0 256zm177.6 62.1C192.8 334.5 218.8 352 256 352s63.2-17.5 78.4-33.9c9-9.7 24.2-10.4 33.9-1.4s10.4 24.2 1.4 33.9c-22 23.8-60 49.4-113.6 49.4s-91.7-25.5-113.6-49.4c-9-9.7-8.4-24.9 1.4-33.9s24.9-8.4 33.9 1.4zm40-89.3l0 0 0 0-.2-.2c-.2-.2-.4-.5-.7-.9c-.6-.8-1.6-2-2.8-3.4c-2.5-2.8-6-6.6-10.2-10.3c-8.8-7.8-18.8-14-27.7-14s-18.9 6.2-27.7 14c-4.2 3.7-7.7 7.5-10.2 10.3c-1.2 1.4-2.2 2.6-2.8 3.4c-.3 .4-.6 .7-.7 .9l-.2 .2 0 0 0 0 0 0c-2.1 2.8-5.7 3.9-8.9 2.8s-5.5-4.1-5.5-7.6c0-17.9 6.7-35.6 16.6-48.8c9.8-13 23.9-23.2 39.4-23.2s29.6 10.2 39.4 23.2c9.9 13.2 16.6 30.9 16.6 48.8c0 3.4-2.2 6.5-5.5 7.6s-6.9 0-8.9-2.8l0 0 0 0zm160 0l0 0-.2-.2c-.2-.2-.4-.5-.7-.9c-.6-.8-1.6-2-2.8-3.4c-2.5-2.8-6-6.6-10.2-10.3c-8.8-7.8-18.8-14-27.7-14s-18.9 6.2-27.7 14c-4.2 3.7-7.7 7.5-10.2 10.3c-1.2 1.4-2.2 2.6-2.8 3.4c-.3 .4-.6 .7-.7 .9l-.2 .2 0 0 0 0 0 0c-2.1 2.8-5.7 3.9-8.9 2.8s-5.5-4.1-5.5-7.6c0-17.9 6.7-35.6 16.6-48.8c9.8-13 23.9-23.2 39.4-23.2s29.6 10.2 39.4 23.2c9.9 13.2 16.6 30.9 16.6 48.8c0 3.4-2.2 6.5-5.5 7.6s-6.9 0-8.9-2.8l0 0 0 0 0 0z"
              />
            </svg>
            <div class="post-type">{{ getNewsType() }}</div>
          </div>

          <div class="line-2 cg-9 flex">
            <svg
              xmlns="http://www.w3.org/2000/svg"
              height="1.25em"
              viewBox="0 0 448 512"
            >
              <!--! Font Awesome Free 6.4.2 by @fontawesome - https://fontawesome.com License - https://fontawesome.com/license (Commercial License) Copyright 2023 Fonticons, Inc. -->
              <path
                d="M0 96C0 78.3 14.3 64 32 64H416c17.7 0 32 14.3 32 32s-14.3 32-32 32H32C14.3 128 0 113.7 0 96zM0 256c0-17.7 14.3-32 32-32H416c17.7 0 32 14.3 32 32s-14.3 32-32 32H32c-17.7 0-32-14.3-32-32zM448 416c0 17.7-14.3 32-32 32H32c-17.7 0-32-14.3-32-32s14.3-32 32-32H416c17.7 0 32 14.3 32 32z"
              />
            </svg>
            <div class="post-type">{{ getCategory() }}</div>
          </div>
        </div>

        <div class="post__title">
          {{ this.news?.Title }}
        </div>

        <p class="post-content">
          {{ this.news?.Content }}
        </p>

        <p class="post-contact-infor">
          {{ this.news?.ContactInfor }}
        </p>

        <div class="post-location cg-6 flex">
          <svg
            data-v-991c812c=""
            xmlns="http://www.w3.org/2000/svg"
            height="0.875em"
            viewBox="0 0 384 512"
          >
            <path
              data-v-991c812c=""
              d="M215.7 499.2C267 435 384 279.4 384 192C384 86 298 0 192 0S0 86 0 192c0 87.4 117 243 168.3 307.2c12.3 15.3 35.1 15.3 47.4 0zM192 128a64 64 0 1 1 0 128 64 64 0 1 1 0-128z"
            ></path>
          </svg>
          <div class="location">{{ this.news?.CityName }}</div>
        </div>

        <div class="img-container">
          <img width="650" :src="ImgLink()" />
        </div>
      </div>
      <div class="post__foot">
        <div class="left flex">
          <div class="flex cg-6">
            <svg
              xmlns="http://www.w3.org/2000/svg"
              height="1.25em"
              viewBox="0 0 512 512"
            >
              <!--! Font Awesome Free 6.4.2 by @fontawesome - https://fontawesome.com License - https://fontawesome.com/license (Commercial License) Copyright 2023 Fonticons, Inc. -->
              <path
                d="M123.6 391.3c12.9-9.4 29.6-11.8 44.6-6.4c26.5 9.6 56.2 15.1 87.8 15.1c124.7 0 208-80.5 208-160s-83.3-160-208-160S48 160.5 48 240c0 32 12.4 62.8 35.7 89.2c8.6 9.7 12.8 22.5 11.8 35.5c-1.4 18.1-5.7 34.7-11.3 49.4c17-7.9 31.1-16.7 39.4-22.7zM21.2 431.9c1.8-2.7 3.5-5.4 5.1-8.1c10-16.6 19.5-38.4 21.4-62.9C17.7 326.8 0 285.1 0 240C0 125.1 114.6 32 256 32s256 93.1 256 208s-114.6 208-256 208c-37.1 0-72.3-6.4-104.1-17.9c-11.9 8.7-31.3 20.6-54.3 30.6c-15.1 6.6-32.3 12.6-50.1 16.1c-.8 .2-1.6 .3-2.4 .5c-4.4 .8-8.7 1.5-13.2 1.9c-.2 0-.5 .1-.7 .1c-5.1 .5-10.2 .8-15.3 .8c-6.5 0-12.3-3.9-14.8-9.9c-2.5-6-1.1-12.8 3.4-17.4c4.1-4.2 7.8-8.7 11.3-13.5c1.7-2.3 3.3-4.6 4.8-6.9c.1-.2 .2-.3 .3-.5z"
              />
            </svg>
            <div class="post__foot-content">{{ CommentCount }} bình luận</div>
          </div>
          <div class="flex cg-6">
            <svg
              xmlns="http://www.w3.org/2000/svg"
              height="1.25em"
              viewBox="0 0 576 512"
            >
              <!--! Font Awesome Free 6.4.2 by @fontawesome - https://fontawesome.com License - https://fontawesome.com/license (Commercial License) Copyright 2023 Fonticons, Inc. -->
              <path
                d="M288 80c-65.2 0-118.8 29.6-159.9 67.7C89.6 183.5 63 226 49.4 256c13.6 30 40.2 72.5 78.6 108.3C169.2 402.4 222.8 432 288 432s118.8-29.6 159.9-67.7C486.4 328.5 513 286 526.6 256c-13.6-30-40.2-72.5-78.6-108.3C406.8 109.6 353.2 80 288 80zM95.4 112.6C142.5 68.8 207.2 32 288 32s145.5 36.8 192.6 80.6c46.8 43.5 78.1 95.4 93 131.1c3.3 7.9 3.3 16.7 0 24.6c-14.9 35.7-46.2 87.7-93 131.1C433.5 443.2 368.8 480 288 480s-145.5-36.8-192.6-80.6C48.6 356 17.3 304 2.5 268.3c-3.3-7.9-3.3-16.7 0-24.6C17.3 208 48.6 156 95.4 112.6zM288 336c44.2 0 80-35.8 80-80s-35.8-80-80-80c-.7 0-1.3 0-2 0c1.3 5.1 2 10.5 2 16c0 35.3-28.7 64-64 64c-5.5 0-10.9-.7-16-2c0 .7 0 1.3 0 2c0 44.2 35.8 80 80 80zm0-208a128 128 0 1 1 0 256 128 128 0 1 1 0-256z"
              />
            </svg>
            <div class="post__foot-content">{{ ViewCount }} lượt xem</div>
          </div>
        </div>
        <div class="right flex">
          <div class="right-share">Share:</div>
          <svg
            @click="shareFB()"
            xmlns="http://www.w3.org/2000/svg"
            height="1.25em"
            viewBox="0 0 512 512"
          >
            <path
              d="M504 256C504 119 393 8 256 8S8 119 8 256c0 123.78 90.69 226.38 209.25 245V327.69h-63V256h63v-54.64c0-62.15 37-96.48 93.67-96.48 27.14 0 55.52 4.84 55.52 4.84v61h-31.28c-30.8 0-40.41 19.12-40.41 38.73V256h68.78l-11 71.69h-57.78V501C413.31 482.38 504 379.78 504 256z"
            />
          </svg>
          <svg
            @click="shareFB()"
            xmlns="http://www.w3.org/2000/svg"
            height="1.25em"
            viewBox="0 0 448 512"
          >
            <path
              d="M416 32H31.9C14.3 32 0 46.5 0 64.3v383.4C0 465.5 14.3 480 31.9 480H416c17.6 0 32-14.5 32-32.3V64.3c0-17.8-14.4-32.3-32-32.3zM135.4 416H69V202.2h66.5V416zm-33.2-243c-21.3 0-38.5-17.3-38.5-38.5S80.9 96 102.2 96c21.2 0 38.5 17.3 38.5 38.5 0 21.3-17.2 38.5-38.5 38.5zm282.1 243h-66.4V312c0-24.8-.5-56.7-34.5-56.7-34.6 0-39.9 27-39.9 54.9V416h-66.4V202.2h63.7v29.2h.9c8.9-16.8 30.6-34.5 62.9-34.5 67.2 0 79.7 44.3 79.7 101.9V416z"
            />
          </svg>
        </div>
      </div>
      <div class="post__comment">
        <div v-for="cmt in comments" :key="cmt" class="comment flex">
          <div class="user__img">
            <img width="36" height="36" :src="cmt.Picture" alt="" />
          </div>
          <div class="comment__body">
            <div class="username">{{ cmt.FullName }}</div>
            <div class="comment__content">
              {{ cmt.Content }}
            </div>
          </div>
        </div>

        <div class="comment__add flex">
          <MSInputText
            title="Bình luận"
            :rows="2"
            v-model:value="comment.Content"
          >
          </MSInputText>
          <button class="main-button" @click="addComment()">Gửi</button>
        </div>
      </div>
    </div>
  </div>
</template>

<script>
import { getAuth, onAuthStateChanged } from "firebase/auth";
export default {
  name: "TPostDetail",
  components: {},
  props: ["PostId"],
  data() {
    return {
      CommentCount: 0,
      header: {
        headers: {
          Authorization: "Bearer ",
        },
      },
      news: {},
      categories: [
        { id: 0, value: "Ví/Giấy tờ" },
        { id: 1, value: "Thú cưng(Chó/Mèo)" },
        { id: 2, value: "Tìm người" },
        { id: 3, value: "Điện thoại/Tablet/Laptop" },
        { id: 4, value: "Chìa khóa" },
        { id: 5, value: "Xe máy/Ô tô" },
        { id: 6, value: "Đồ vật khác" },
      ],
      imagePreviewUrl: "",
      selectedImage: null,
      isProcessing: false,
      comment: {},
      comments: null,
    };
  },
  beforeUpdate() {
    if (this.comments) {
      this.CommentCount = this.comments.length;
    }
  },
  computed: {
    ViewCount() {
      if (this.news.ViewCount / 1000 > 1) {
        let hundred = Math.floor(this.news.ViewCount / 100);

        return hundred / 10 + "k";
      }
      return this.news.ViewCount;
    },
    PhotoURL: {
      get() {
        return this.$msCookies.get("photoURL");
      },
    },
  },
  async created() {
    if (this.PostId) {
      const response = await this.$msAxios(
        "get",
        this.$msApi.NewsApi.Get(this.PostId)
      );
      this.news = response.data;
      this.comment.NewsId = this.news.NewsId;
      this.loadComments();
    } else {
      this.$router.push("/");
    }
  },
  mounted() {
    this.auth = getAuth();

    onAuthStateChanged(this.auth, () => {
      if (!this.isLoggedIn()) {
        this.$router.push("/TSignIn");
      }
    });
  },
  methods: {
    shareFB() {
      window.open(
        "https://www.facebook.com/sharer/sharer.php?u=" + window.location.href,
        "_blank"
      );
    },
    async loadComments() {
      if (this.comment.NewsId) {
        const response2 = await this.$msAxios(
          "get",
          this.$msApi.CommentApi.Paging,
          {
            params: {
              // Kích thước của trang
              pageSize: 100,
              // vị trí trang
              pageNumber: 1,
              NewsId: this.comment.NewsId,
            },
          }
        );
        this.comments = response2.data.Data;
      }
    },
    isLoggedIn() {
      this.auth = getAuth();
      if (!this.auth?.currentUser?.accessToken) {
        const cookie = this.$msCookies.get("accessToken");
        return cookie;
      } else {
        this.$msCookies.set("accessToken", this.auth.currentUser.accessToken);
      }
      const cookie = this.$msCookies.get("accessToken");
      this.header = {
        headers: {
          Authorization: "Bearer " + cookie,
        },
      };

      return true;
    },
    async addComment() {
      await this.$msAxios(
        "post",
        this.$msApi.CommentApi.Post,
        this.comment,
        this.header
      );
      this.comment.Content = "";
      await this.loadComments();
    },
    homeOnClick() {
      this.$router.push("/");
    },
    ImgLink() {
      return this.news?.ImgLink
        ? this.$msApi.NewsApi.GetImg(this.news.ImgLink)
        : "";
    },
    getNewsType() {
      if (this.news?.NewsType == 0) {
        return "Tin cần tìm";
      }
      return "Tin nhặt được";
    },
    getCategory() {
      let cate = this.categories[0].value;
      for (const index in this.categories) {
        const category = this.categories[index];
        if (this.news?.Category == category.id) {
          cate = category.value;
          break;
        }
      }

      return cate;
    },
  },
};
</script>

<style lang="scss" scoped>
.center-container {
  width: 100%;
  display: flex;
  justify-content: center;
  padding-top: 12px;

  .post {
    border-radius: 4px;
    margin-bottom: 12px;
    background: white;
    width: 650px;

    .post__head {
      display: flex;
      justify-content: space-between;
      padding: 16px;
      border-bottom: 1px solid var(--border-color);

      .left {
        .user__img img {
          border-radius: 50%;
        }
      }
      .right {
        display: flex;
        align-items: center;
        margin-right: 7px;
      }
    }
    .post__body {
      .post__infor {
        padding: 8px 16px;
        * {
          font-size: 15px !important;
        }

        .line-1,
        .line-2 {
          height: 30px;
          align-items: center;
        }
      }

      .post__title {
        padding: 8px 16px;
        font-size: 18px;
        line-height: 1.5rem;
        font-weight: bold;
      }

      .post-content {
        padding: 8px 16px;
        font-size: 16px;
      }
      .post-contact-infor {
        padding: 8px 16px;
        font-size: 16px;
      }
      .post-location {
        padding: 8px 16px;
        align-items: center;

        .location {
          font-size: 15px;
          color: #333;
          font-weight: 600;
        }
      }
      .img-container {
        display: flex;
        align-items: center;
        justify-content: center;
      }
    }
    .post__foot {
      border-bottom: 1px solid #ccc;
      display: flex;
      padding: 8px 12px;
      justify-content: space-between;
      .left {
        column-gap: 12px;
        .flex * {
          display: flex;
          align-items: center;
        }
      }
      .right {
        align-items: center;
        column-gap: 6px;
        .right-share {
          display: flex;
          align-items: center;
        }
        svg {
          cursor: pointer;
        }
      }
    }
    .post__comment {
      gap: 12px;
      display: flex;
      flex-direction: column;
      padding: 24px;
      border-radius: 5px;
      .comment {
        gap: 12px;

        .user__img img {
          border-radius: 50%;
        }
        .comment__body {
          border-radius: 15px;
          padding: 12px;
          display: flex;
          flex-direction: column;
          gap: 6px;
          width: 554px;
          background-color: #f3f3f3;
          .username {
            font-weight: 600;
            text-transform: capitalize;
          }
        }
      }
    }
  }
}
</style>
<style lang="scss" >
.post__comment {
  .comment__add {
    align-items: center;
  }
  .input {
    .input__label {
      padding-bottom: 6px;
    }
  }
}
</style>