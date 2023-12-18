<template>
  <div class="center">
    <div class="new-post">
      <div class="title">Đăng tin mới</div>
      <div class="post-form">
        <MSInputText
          :isValidating="isValidating"
          :errorMessage="titleMessage"
          title="Tiêu đề"
          :isRequired="true"
          :rows="2"
          placeholder="VD: Rơi ví và một số giấy tờ mang tên Nguyễn Văn A ở Cầu Giấy - Hà Nội"
          v-model:value="news.Title"
        >
        </MSInputText>

        <div class="flex cg-12">
          <div class="w-1/2">
            <MSInputRadio
              title="Chọn loại tin"
              direction="column"
              :items="[
                { id: 0, value: 'Tin cần tìm' },
                { id: 1, value: 'Tin nhặt được' },
              ]"
              v-model:id="news.NewsType"
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

          <div class="w-1/2">
            <MSInputCombobox
              :isValidating="isValidating"
              :errorMessage="cityMessage"
              :isRequired="true"
              title="Chọn khu vục"
              placeholder="Tỉnh/Thành phố"
              propText="Name"
              propValue="CityId"
              v-model:id="news.CityId"
              :apiPaging="CityPagingApi"
              :apiGetById="CityGetApi"
              @valueOnChange="cityOnChange"
            >
            </MSInputCombobox>

            <MSInputCombobox
              propText="Name"
              propValue="DistrictId"
              placeholder="Quận/Huyện"
              v-model:id="news.DistrictId"
              :apiPaging="DistrictPagingApi"
              :apiGetById="DistrictGetApi"
              @valueOnChange="districtOnChange"
            >
            </MSInputCombobox>

            <MSInputCombobox
              propText="Name"
              propValue="CommuneId"
              placeholder="Phường/Xã/Thị trấn"
              v-model:id="news.CommuneId"
              :apiPaging="CommunePagingApi"
              :apiGetById="CommuneGetApi"
            >
            </MSInputCombobox>
          </div>
        </div>

        <div class="item-upload-image">
          <label class="input-upload-area">
            <input
              type="file"
              accept="image/*"
              @change="previewImage"
              ref="fileInput"
              :disabled="!!imagePreviewUrl"
            />
            <div class="upload-images">
              <div class="flex justify-center">
                <div class="icon-container">
                  <div class="icon-upload-black"></div>
                </div>
              </div>
              <span>Tải ảnh lên(không bắt buộc)</span>
            </div>
          </label>
          <div v-if="imagePreviewUrl" class="preview-images">
            <img :src="imagePreviewUrl" alt="" />
            <button
              type="button"
              class="btn-close-image"
              @click="cancelUploadImg()"
            >
              <div class="icon-container">
                <div class="icon-x-black"></div>
              </div>
            </button>
          </div>
        </div>

        <MSInputText
          :isValidating="isValidating"
          :errorMessage="contentMessage"
          v-model:value="news.Content"
          title="Nội dung"
          :isRequired="true"
          :rows="10"
          placeholder="Nội dung phải đủ các thành phần sau:
    - Thời gian xảy ra sự việc
    - Địa điểm
    - Liệt kê các vật liên quan"
        ></MSInputText>

        <MSInputText
          :isValidating="isValidating"
          :errorMessage="contactInforMessage"
          v-model:value="news.ContactInfor"
          title="Thông tin liên lạc"
          :isRequired="true"
          :rows="3"
          placeholder="- Thông tin để người khác liên lạc với bạn (Họ và tên, Số điện thoại, vv...)"
        ></MSInputText>
        <div class="post-control">
          <button
            class="main-button"
            :disabled="isProcessing"
            @click="postOnClick()"
          >
            Đăng tin
          </button>
        </div>
      </div>
    </div>
  </div>
</template>

<script>
import { getAuth, onAuthStateChanged } from "firebase/auth";
export default {
  name: "TPostAdd",
  components: {},
  props: [],
  data() {
    return {
      isValidating: false,
      titleMessage: "Tiêu đề không được để trống",
      cityMessage: "Thành phố không được để trống",
      contentMessage: "Nội dung không được để trống",
      contactInforMessage: "Thông tin liên lạc không được để trống",
      header: {
        headers: {
          Authorization: "Bearer ",
        },
      },
      news: {
        Category: 0,
        NewsType: 0,
      },
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
    };
  },
  computed: {
    CityPagingApi: {
      get() {
        return this.$msApi.CityApi.Paging;
      },
    },

    DistrictPagingApi: {
      get() {
        if (this.news?.CityId) {
          return this.$msApi.DistrictApi.Paging + "/" + this.news?.CityId;
        }
        return null;
      },
    },

    CommunePagingApi: {
      get() {
        if (this.news?.DistrictId) {
          return this.$msApi.CommuneApi.Paging + "/" + this.news?.DistrictId;
        }
        return null;
      },
    },
  },
  created() {},
  mounted() {
    this.auth = getAuth();

    onAuthStateChanged(this.auth, () => {
      if (!this.isLoggedIn()) {
        this.$router.push("/TSignIn");
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
    async postOnClick() {
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
        this.$router.push("/");
      }
    },
    validate() {
      console.log(this.news);
      this.isValidating = true;

      const notEmpty =
        this.news.Title &&
        this.news.CityId &&
        this.news.Content &&
        this.news.ContactInfor;

      const isValid = notEmpty;
      if (isValid) {
        this.isValidating = false;
      }
      return isValid;
    },
    cancelUploadImg() {
      this.imagePreviewUrl = "";
      this.selectedImage = null;
    },
    previewImage(event) {
      const file = event.target.files[0];
      if (file) {
        const reader = new FileReader();

        reader.onload = (e) => {
          this.imagePreviewUrl = e.target.result;
          this.selectedImage = file;
        };

        reader.readAsDataURL(file);
      }
    },
    async uploadImage() {
      if (!this.selectedImage) return;

      const formData = new FormData();
      formData.append("file", this.selectedImage);

      // Gọi API tải lên ở đây
      // Ví dụ:
      const response = await this.$msAxios(
        "post",
        this.$msApi.NewsApi.UploadImg,
        formData
      );

      return response.data;
    },
    /**
     * Hàm thực hiện khi Huyện thay đổi
     * @param {Object} district
     * Author: LeDucTiep (12/07/2023)
     */
    districtOnChange(district) {
      console.log("district", district);
      this.CommuneId = null;
    },

    /**
     * Hàm thực hiện khi thành phố thay đổi
     * @param {Object} city
     * Author: LeDucTiep (12/07/2023)
     */
    cityOnChange(city) {
      console.log("city: ", city);
      this.CommuneId = null;
      this.DistrictId = null;
    },
    /**
     * Hàm lấy API lấy thành phố theo Id quốc gia và id thành phố
     * @param {Guid} cityId Id của thành phố
     * @returns API lấy thành phố theo quốc gia và id thành phố
     * Author: LeDucTiep (14/07/2023)
     */
    CityGetApi(cityId) {
      return this.$msApi.CityApi.Get(cityId);
    },

    /**
     * Hàm lấy API lấy thông tin huyện theo Id thành phố và id huyện
     * @param {Guid} districtId Id của huyện
     * @returns API lấy thông tin huyện theo Id thành phố và id huyện
     * Author: LeDucTiep (14/07/2023)
     */
    DistrictGetApi(districtId) {
      return this.$msApi.DistrictApi.Get(this.CityId) + "/" + districtId;
    },

    /**
     * Hàm lấy API lấy thông tin xã theo id xã và id huyện
     * @param {Guid} communeId Id của xã
     * @returns API lấy thông tin xã theo id xã và id huyện
     * Author: LeDucTiep (14/07/2023)
     */
    CommuneGetApi(communeId) {
      return this.$msApi.CommuneApi.Get(this.DistrictId) + "/" + communeId;
    },
  },
};
</script>

<style lang="scss" >
.center {
  width: 100%;
  display: flex;
  justify-content: center;
  align-items: center;
}
.new-post {
  margin-top: 154px;
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
    margin-bottom: 23px;
  }
  &:nth-child(2) {
    margin-top: 0px;
    margin-bottom: 23px;
  }
}
</style>