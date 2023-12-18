<template>
  <div class="new-post">
    <div class="title">Đăng tin mới</div>
    <div class="post-form">
      <div class="flex cg-12">
        <div class="w-1/2">
          <MSInputRadio
            title="Chọn loại tin"
            direction="column"
            :items="[
              { id: 1, value: 'Tin cần tìm' },
              { id: 2, value: 'Tin nhặt được' },
            ]"
            v-model:id="news.article_type"
          ></MSInputRadio>
          <MSInputCombobox
            title="Chọn danh mục"
            propText="value"
            propValue="id"
            v-model:id="news.Category"
            :listItems="categories"
          >
          </MSInputCombobox>
        </div>
      </div>

      <div class="post-control">
        <button
          class="main-button"
          :disabled="isProcessing"
          @click="postOnClick()"
        >
          Lấy dữ liệu
        </button>
      </div>
    </div>
  </div>
  <div id="data-container"></div>
</template>
  
  <script>
import { getAuth, onAuthStateChanged } from "firebase/auth";
export default {
  name: "TCrawlData",
  components: {},
  props: [],
  data() {
    return {
      header: {
        headers: {
          Authorization: "Bearer ",
        },
      },
      news: {
        Category: "vigiay-to",
        article_type: 1,
      },
      categories: [
        { id: "vigiay-to", value: "Ví/Giấy tờ" },
        { id: "thu-cung", value: "Thú cưng(Chó/Mèo)" },
        { id: "tim-nguoi", value: "Tìm người" },
        { id: "dien-thoaitabletlaptop", value: "Điện thoại/Tablet/Laptop" },
        { id: "chia-khoa", value: "Chìa khóa" },
        { id: "xe-co", value: "Xe máy/Ô tô" },
        { id: "do-vat-khac", value: "Đồ vật khác" },
      ],
      imagePreviewUrl: "",
      selectedImage: null,
      isProcessing: false,
    };
  },
  computed: {},
  created() {},
  mounted() {
    this.auth = getAuth();

    onAuthStateChanged(this.auth, () => {
      if (!this.isLoggedIn()) {
        console.log("");
      }
    });
  },
  methods: {
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

    async getData(page) {
      const url = `https://timdothatlac.vn/ket-qua-tim-kiem?article_type=${this.news.article_type}&keywords=&category=${this.news.Category}&province_id=&page=${page}`;
      const response = await this.$msAxios("get", url, {
        headers: {
          "Access-Control-Allow-Origin": "*",
        },
      });
      document.getElementById("data-container").innerHTML = response.data;

      const list = document.querySelectorAll(".posts.list");
      console.log(list);
      const aNews = list[0];
      const imgLink = aNews.querySelector("a > img").src;
      console.log(imgLink);
      return response;
    },
    async postOnClick() {
      await this.getData();
      if (this.news) return;
      const isValid = this.validate();
      if (isValid) {
        const imgLink = await this.uploadImage();

        const dataSend = {
          ...this.news,
          ImgLink: imgLink,
        };
        // Gửi thông tin để tạo bai viet mới
        const response = await this.$msAxios(
          "post",
          this.$msApi.NewsApi.Post,
          dataSend,
          this.header
        );

        if (!response) {
          this.$msEmitter.emit("addNotice", {
            type: this.$msEnum.NoticeType.Error,
            message: this.$t("Notice.Post.Error"),
          });
        } else if (
          response.status == this.$msEnum.HttpStatusCode.CreatedSuccess
        ) {
          this.$msEmitter.emit("addNotice", {
            type: this.$msEnum.NoticeType.Success,
            message: this.$t("Notice.Post.Success"),
          });
        }
      }
    },
  },
};
</script>
  
  <style lang="scss" scoped >
.center {
  width: 100%;
  display: flex;
  justify-content: center;
  align-items: center;
}
.new-post {
  margin-top: 4px;
  width: 777px;
  padding: 24px;
  .title {
    font-size: 16px;
    font-weight: bold;
    margin-bottom: 12px;
  }
  .post-form {
    * {
      font-size: 14px !important;
    }

    background-color: #fff;
    padding: 24px;
    display: flex;
    flex-direction: column;
    column-gap: 12px;

    .input {
      .input__label {
        margin-bottom: 8px;
      }
    }

    .post-control {
      margin-top: 24px;
      display: flex;
      justify-content: flex-end;
    }
  }
}

.item-upload-image {
  display: flex;
  align-items: center;
  justify-content: center;

  &:has(.preview-images) {
    .input-upload-area * {
      opacity: 0%;
      cursor: default !important;
    }
  }

  .input-upload-area {
    display: flex;

    .upload-images {
      border: 2px dashed #d9d9d9;
      border-radius: 5px;
      cursor: pointer;
      display: flex;
      flex-flow: column;
      margin: 0 auto;
      max-width: 290px;
      overflow: hidden;
      padding-top: 66px;
      text-align: center;
      height: 200px;
      min-width: 250px;
      position: relative;
    }

    input[type="file"] {
      height: 1px;
      opacity: 0;
      width: 1px;
    }
  }

  position: relative;

  .preview-images {
    background: #333;
    box-shadow: 0 0 5px 2px rgba(0, 0, 0, 0.2);
    margin: 0 auto;
    max-width: 290px;
    overflow: hidden;
    text-align: center;
    height: 200px;
    border-radius: 4px;
    display: block;
    position: absolute;
    top: 0;
    .btn-close-image {
      position: absolute;
      right: 0;
      border-radius: 4px;
      top: 0;
    }

    img {
      height: 200px;
      border-radius: 4px;
      width: auto;
    }
  }
}

textarea {
  line-height: 20px !important;
}

#app
  > div
  > div.body
  > div
  > div
  > div
  > div.post-form
  > div.flex.cg-12
  > div:nth-child(1)
  > div:nth-child(1) {
  .list-item {
    row-gap: 6px;
  }
}
.input {
  margin-top: 12px;
  &:first-child {
    margin-top: 0px;
    margin-bottom: 12px;
  }
}
</style>