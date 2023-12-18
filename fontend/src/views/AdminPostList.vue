<template>
  <div v-show="isShowSetting" class="setting">
    <div class="nav">
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
          <button class="main-button" @click="loadPostList()">Tìm kiếm</button>
        </div>
      </div>
    </div>
  </div>
  <div class="head">
    <div class="left">
      <p>
        {{ $t("Cash.Selected") }}
        <span>{{ selectedNewsIdList.length }}</span>
      </p>
      <button
        class="cancel-select link-button"
        @click="selectedList = {}"
        :disabled="selectedNewsIdList.length == 0"
      >
        {{ $t("Cash.UnSelect") }}
      </button>
      <button
        class="link-button"
        :disabled="selectedNewsIdList.length == 0"
        @click="acceptSelected()"
      >
        Duyệt
      </button>

      <button
        class="link-button"
        :disabled="selectedNewsIdList.length == 0"
        @click="hideSelected()"
      >
        Ẩn
      </button>
    </div>
    <div class="right">
      <MSInputSearch
        v-model:value="filter.SearchTerm"
        :placeholder="this.$t('Placeholder.InputSearch')"
        @inputSearchOnSubmit="loadPostList()"
      ></MSInputSearch>
      <div
        class="icon-container"
        @mousemove="
          (event) =>
            $msEmitter.emit('showTooltip', event, this.$t('Button.ReloadList'))
        "
        @mouseout="$msEmitter.emit('hideTooltip')"
      >
        <div class="icon-reload" @click="loadPostList()"></div>
      </div>

      <div
        class="icon-container"
        @mousemove="
          (event) =>
            $msEmitter.emit('showTooltip', event, this.$t('Button.Setting'))
        "
        @mouseout="$msEmitter.emit('hideTooltip')"
        @click="toggleSetting()"
      >
        <div class="icon-setting-gray"></div>
      </div>

      <button class="extra-button" @click="toggleSetting()">
        {{ $t("Button.Utilities") }}
      </button>
    </div>
  </div>

  <div class="body flex flex-column">
    <div class="sticky-table">
      <table class="table">
        <thead>
          <tr>
            <th>
              <MSCheckBox
                :isTableHead="!isAllRowsSelected"
                :value="selectedNewsIdList.length != 0"
                @onChangeValue="(isChecked) => toggleAllRows(isChecked)"
              ></MSCheckBox>
            </th>
            <th
              v-for="tableHeader in tableHeaders"
              :key="tableHeader.text"
              @mousemove="tableHeaderOnHover($event, tableHeader)"
              @mouseout="tableHeaderOnUnhover(tableHeader)"
            >
              {{ tableHeader.text }}
            </th>
          </tr>
        </thead>

        <tbody v-if="postList == null">
          <tr v-for="row in 20" :key="row">
            <td>
              <div
                :style="{ width: '16px', height: '16px' }"
                class="td-loading"
              ></div>
            </td>
            <td v-for="i in this.columnList.length + 1" :key="i">
              <div class="td-loading"></div>
            </td>
          </tr>
        </tbody>

        <tbody v-else>
          <tr
            v-for="news in postList"
            :key="news.NewsId"
            :style="{
              color:
                news.NewsStatus == 0
                  ? 'black'
                  : news.NewsStatus == 1
                  ? '#14A200'
                  : 'gray',
            }"
          >
            <td>
              <MSCheckBox
                :value="!!selectedList[news.NewsId]"
                @onChangeValue="
                  (isChecked) => {
                    setIdIntoSelectedList(news.NewsId, isChecked);
                  }
                "
              ></MSCheckBox>
            </td>
            <td
              v-for="x in getDataRow(news)"
              :key="x"
              @dblclick="receiptListOnDoubleClick(news)"
            >
              <span
                v-if="x?.length > 38"
                class="table-data"
                @mousemove="(event) => $msEmitter.emit('showTooltip', event, x)"
                @mouseout="$msEmitter.emit('hideTooltip')"
              >
                {{ x.substring(0, 38) + "..." }}
              </span>
              <span v-else class="table-data">
                {{ x }}
              </span>
            </td>

            <td>
              <div class="flex">
                <button
                  class="link-button"
                  @click="receiptListOnDoubleClick(news)"
                >
                  {{ this.$t("Button.View") }}
                </button>
                <div
                  class="icon-container"
                  @click="(event) => toggleMenuFeature(event, news)"
                >
                  <div class="icon-triangle-down-black"></div>
                </div>
              </div>
            </td>
          </tr>
        </tbody>
      </table>
    </div>
    <div class="table__footer">
      <div class="table__footer--left">
        <div class="table__numrows">
          {{
            $t("ReceiptList.TotalNumberOfRecordsIs", [
              TotalRecord ? numeralFormat(TotalRecord, "0,0") : 0,
            ])
          }}
        </div>
      </div>
      <div class="table__footer--right">
        <div class="table__choose-rowcount">
          <p>
            {{ $t("ReceiptList.NumberOfRecordOutOfPageIs") }}
          </p>

          <div class="table__maxrow">
            <MSCombobox
              :items="[10, 20, 30, 50, 100]"
              v-model:value="pageSizeSelected"
              :isSmall="true"
              inputType="number"
              :max="999"
              :min="10"
            ></MSCombobox>
          </div>
        </div>
        <div class="table__recordnumber">
          {{
            $t("ReceiptList.FromXToYRecords", [
              pageNumberSelected
                ? pageNumberSelected * pageSizeSelected > TotalRecord
                  ? TotalRecord
                  : pageNumberSelected * pageSizeSelected
                : 0,
              TotalRecord ? numeralFormat(TotalRecord, "0,0") : 0,
            ])
          }}
        </div>

        <div
          class="icon-container"
          @click="if (1 < pageNumberSelected && postList) previousPage();"
        >
          <div
            v-if="1 == pageNumberSelected || !postList"
            class="icon-arrow-left-gray"
            :class="{ 'cursor-not-allowed': postList }"
          ></div>
          <div v-else class="icon-arrow-left-black"></div>
        </div>
        <div
          class="icon-container"
          @click="
            if (
              !(
                TotalPage == pageNumberSelected ||
                !postList ||
                (pageNumberSelected
                  ? pageNumberSelected * pageSizeSelected > TotalRecord
                    ? TotalRecord
                    : pageNumberSelected * pageSizeSelected
                  : 0) == TotalRecord
              )
            )
              nextPage();
          "
        >
          <div
            v-if="
              TotalPage == pageNumberSelected ||
              !postList ||
              (pageNumberSelected
                ? pageNumberSelected * pageSizeSelected > TotalRecord
                  ? TotalRecord
                  : pageNumberSelected * pageSizeSelected
                : 0) == TotalRecord
            "
            class="icon-arrow-right-gray"
            :class="{ 'cursor-not-allowed': postList }"
          ></div>
          <div v-else class="icon-arrow-right-black"></div>
        </div>
      </div>
    </div>
  </div>

  <MenuFeature></MenuFeature>
</template>
    
<script>
export default {
  name: "AdminPostList",
  data() {
    return {
      isShowSetting: false,
      filter: {
        Category: null,
        NewsType: null,
        ProvinceId: "",
        SearchTerm: "",
      },
      receiptFormKey: 0,
      tableHeaders: [
        {
          text: "Trạng thái",
        },
        {
          text: "Tiêu đề",
        },
        {
          text: "Loại tin",
        },
        {
          text: "Nội dung",
        },
        {
          text: "Thông tin liên hệ",
        },
        {
          text: "Thành phố",
        },
        {
          text: "Huyện",
        },
        {
          text: "Xã",
        },
        {
          text: "Chức năng",
        },
      ],
      postList: null,
      columnList: [
        "NewsStatus",
        "Title",
        "NewsType",
        "Content",
        "ContactInfor",
        "CityName",
        "DistrictName",
        "CommuneName",
      ],
      // Danh sách phần tử đã chọn
      selectedList: {},
      // Thông tin của dialog hiện lên
      dialog: {},
      TotalRecord: 0,

      TotalPage: 0,

      pageSizeSelected: 20,

      pageNumberSelected: 1,

      ReceiptOnMenu: null,
      header: {
        headers: {
          Authorization: "Bearer " + this.$msCookies.get("accessToken"),
        },
      },
      provinces: [{ CityId: "", Name: "Toàn quốc" }],
    };
  },
  watch: {
    async pageSizeSelected() {
      this.pageNumberSelected = 1;
      await this.loadPostList();
    },
  },
  computed: {
    selectedNewsIdList() {
      let list = [];
      for (let id in this.selectedList) {
        if (this.selectedList[id]) {
          list.push(id);
        }
      }
      return list;
    },
    isAllRowsSelected() {
      if (!this.postList) return false;

      // Không có phần tử nào được chọn
      if (Object.keys(this.selectedList).length == 0) return false;

      for (const index in this.postList) {
        let news = this.postList[index];
        if (!this.selectedList[news.NewsId]) {
          return false;
        }
      }
      return true;
    },
  },
  async created() {
    await this.checkPermission();
    this.loadPostList();
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
  beforeUnmount() {},
  methods: {
    resetFilter() {
      this.filter = {
        Category: null,
        NewsType: null,
        ProvinceId: 0,
      };
    },
    toggleSetting() {
      this.isShowSetting = !this.isShowSetting;
    },
    async checkPermission() {
      const response = await this.$msAxios(
        "get",
        this.$msApi.AccountApi.CheckToken,
        this.header
      );
      if (response.data.Role != 1) {
        this.$router.push("/");
      }
    },
    /**
     * Hàm thực hiện sau khi sửa phiếu thu
     * @param {Object} news Phiếu thu
     * Author: LeDucTiep (12/07/2023)
     */
    afterPutReceipt(news) {
      // Nếu có khách hàng
      if (this.postList.length > 0)
        // Duyệt qua từng khách hàng
        for (let index = 0; index < this.postList.length; index++)
          // Nếu khách hàng có mã khớp với khách hàng đã sửa
          if (this.postList[index].EmployeeId == news.EmployeeId) {
            // Cập nhật lại thông tin trên giao diện
            this.postList[index] = news;
            return;
          }
    },

    /**
     * Hàm thực hiện sau khi thêm phiếu thu
     * @param {Object} news Phiếu thu
     * Author: LeDucTiep (12/07/2023)
     */
    afterPostReceipt(news) {
      // Thêm news mới vào đầu danh sách
      this.postList.unshift(news);
      this.TotalRecord++;
    },

    /**
     * Hàm reset form
     * Author: LeDucTiep (12/07/2023)
     */
    resetAddReceiptForm() {
      this.receiptFormKey++;
    },

    /**
     * Hàm xóa danh sách phiếu thu
     * @param {Object} dataSend
     * Author: LeDucTiep (12/07/2023)
     */
    async deleteListReceipts() {
      // Gọi api xóa
      const response = await this.$msAxios(
        "delete",
        this.$msApi.ReceiptApi.DeleteMany,
        {
          data: this.selectedNewsIdList,
        }
      );

      // Request lỗi
      if (!response) {
        this.$msEmitter.emit("addNotice", {
          type: this.$msEnum.NoticeType.Error,
          message: this.$t("Error.UnknownError"),
        });
        return false;
      }

      // Số lượng bị xóa
      const deleteCount = response.data;

      // Lỗi
      if (deleteCount == 0) {
        this.$msEmitter.emit("addNotice", {
          type: this.$msEnum.NoticeType.Error,
          message: this.$t("Notice.Delete.Error"),
        });
        return false;
      }

      // Thành công
      this.$msEmitter.emit("addNotice", {
        type: this.$msEnum.NoticeType.Success,
        message: this.$t("Notice.Delete.ManySuccess", [deleteCount]),
      });

      // Xóa trên giao diện
      for (const i in this.selectedNewsIdList) {
        let receiptId = this.selectedNewsIdList[i];

        const index = this.getIndexOfReceiptId(receiptId);
        // Nếu tìm thấy thì mới xóa
        if (index > -1) {
          // Xóa đi 1 bản ghi từ vị trí index
          this.postList.splice(index, 1);
          // giản số lượng bản ghi của trang hiện tại
          this.TotalRecord--;
        }
      }

      // Xóa các id đã chọn
      this.selectedList = {};

      // Nếu xóa hết toàn trang thì load lại trang
      if (this.postList.length == 0) {
        this.loadPostList();
      }
    },

    /**
     * Hàm lấy index của phiếu thu trong danh sách
     * @param {Guid} id Id của phiếu thu
     * @returns index của phiếu thu trong danh sách
     * Author: LeDucTiep (12/07/2023)
     */
    getIndexOfReceiptId(id) {
      // Nếu danh sách nhân viên không tồn tại thì trở về
      if (!this.postList) return -1;

      // Nếu danh sách không có nhân viên thì trở về
      if (this.postList.length == 0) return -1;

      // Tìm và trả về index nếu thấy
      for (let i = 0; i < this.postList.length; i++) {
        if (this.postList[i].NewsId == id) {
          return i;
        }
      }

      // Không tìm thấy
      return -1;
    },

    /**
     * Hàm xóa phiếu thu
     * @param {Object} news Phiếu thu
     * @returns bool
     * Author: LeDucTiep (12/07/2023)
     */
    async deleteReceipt(news) {
      // Gọi api xóa
      const response = await this.$msAxios(
        "delete",
        this.$msApi.ReceiptApi.Delete(news.NewsId)
      );

      if (!response) {
        this.$msEmitter.emit("addNotice", {
          type: this.$msEnum.NoticeType.Error,
          message: this.$t("Error.UnknownError"),
        });
        return false;
      }

      const deleteCount = response.data;

      // Lỗi
      if (deleteCount == 0) {
        this.$msEmitter.emit("addNotice", {
          type: this.$msEnum.NoticeType.Error,
          message: this.$t("Notice.Delete.Error"),
        });
        return false;
      }

      // Thành công
      this.$msEmitter.emit("addNotice", {
        type: this.$msEnum.NoticeType.Success,
        message: this.$t("Notice.Delete.Success"),
      });

      // Xóa trên giao diện
      const index = this.postList?.indexOf(news);
      // Nếu tìm thấy thì mới xóa
      if (index > -1) {
        // Xóa đi 1 bản ghi từ vị trí index
        this.postList.splice(index, 1);
        // giản số lượng bản ghi của trang hiện tại
        this.TotalRecord--;
      }
    },

    /**
     * Hàm hiển thị thông báo tính năng chưa hỗ trọ
     * Author: LeDucTiep (12/07/2023)
     */
    notSupported() {
      this.$msEmitter.emit(
        "showDialog",
        this.$msEnum.DialogType.FeatureNotSupported,
        [this.$t("Dialog.FeatureNotSupported")]
      );
    },

    /**
     * Hàm lấy thông tin của một dòng
     * @param {Object} news phiếu thu
     * @returns Thông tin của 1 dòng
     * Author: LeDucTiep (12/07/2023)
     */
    getDataRow(news) {
      const data = {};
      for (const colIndex in this.columnList) {
        for (const field in news) {
          const col = this.columnList[colIndex];
          if (col == field) {
            if (col == "NewsType") {
              data[col] = news[col] == 0 ? "Tin cần tìm" : "Tin nhặt được";
            } else if (col == "NewsStatus") {
              data[col] =
                news[col] == 0
                  ? "Khởi tạo"
                  : news[col] == 1
                  ? "Đăng tin"
                  : "Ẩn";
            } else data[col] = news[col];
          }
        }
      }
      return data;
    },

    /**
     * Hàm tải danh sách phiếu thu
     * Author: LeDucTiep (12/07/2023)
     */
    async loadPostList() {
      this.postList = null;
      const response = await this.$msAxios("get", this.$msApi.NewsApi.Paging, {
        params: {
          // Kích thước của trang
          pageSize: this.pageSizeSelected,
          // vị trí trang
          pageNumber: this.pageNumberSelected,
          // Từ khóa tìm kiếm
          SearchTerm: this.filter.SearchTerm,
          Category: this.filter.Category,
          NewsType: this.filter.NewsType,
          ProvinceId: this.filter.ProvinceId,
        },
      });

      if (response?.data) {
        this.TotalRecord = response.data.TotalRecord;
        this.postList = response?.data?.Data.map((obj) => {
          if (obj.AccountingDate)
            obj.AccountingDate = this.$msDateFormater.convertIsoToDDMMYYY(
              obj.AccountingDate
            );
          if (obj.ReceiptDate)
            obj.ReceiptDate = this.$msDateFormater.convertIsoToDDMMYYY(
              obj.ReceiptDate
            );
          if (obj.TotalMoney)
            obj.TotalMoney = obj.TotalMoney
              ? this.numeralFormat(obj.TotalMoney, "0,0")
              : 0;

          return obj;
        });
      }
    },

    /**
     * Hàm toogle tất cả các dòng
     * @param {bool} isChecked
     * Author: LeDucTiep (12/07/2023)
     */
    toggleAllRows(isChecked) {
      for (const index in this.postList) {
        let news = this.postList[index];
        this.selectedList[news.NewsId] = isChecked;
      }
    },

    /**
     * Hàm hiện tooltip khi hover lên table header
     * Author: LeDucTiep (29/05/2023)
     */
    tableHeaderOnHover(event, tableHeader) {
      if (tableHeader.hint)
        this.$msEmitter.emit("showTooltip", event, tableHeader.hint);
    },

    /**
     * Hàm ẩn tooltip khi hover lên table header
     * Author: LeDucTiep (29/05/2023)
     */
    tableHeaderOnUnhover(tableHeader) {
      if (tableHeader.hint) {
        this.$msEmitter.emit("hideTooltip");
      }
    },

    /**
     * Hàm tích chọn một dòng
     * Author: LeDucTiep (06/05/2023)
     */
    setIdIntoSelectedList(id, isChecked) {
      if (!isChecked && this.selectedList[id]) {
        this.selectedList[id] = false;
      } else if (isChecked && !this.selectedList[id]) {
        this.selectedList[id] = true;
      }
    },

    /**
     * Hàm thực hiện khi nhân đúp vào một dòng
     */
    receiptListOnDoubleClick(news) {
      this.$router.push(`/TPostDetail/${news.NewsId}`);
    },
    /**
     * Hàm xóa bản ghi đã select
     * Author: LeDucTiep (12/07/2023)
     */
    async acceptSelected() {
      // Hiện thông báo confirm
      for (const index in this.postList) {
        const post = this.postList[index];
        if (this.selectedList[post.NewsId]) await this.accept(post);
      }
    },

    async hideSelected() {
      // Hiện thông báo confirm
      for (const index in this.postList) {
        const post = this.postList[index];
        if (this.selectedList[post.NewsId]) await this.hide(post);
      }
    },

    async accept(news) {
      if (!news.NewsId) return;

      news.NewsStatus = 1;

      const response = await this.$msAxios(
        "put",
        this.$msApi.NewsApi.Put(news.NewsId),
        news
      );

      if (response?.data) {
        this.$msEmitter.emit("addNotice", {
          type: this.$msEnum.NoticeType.Success,
          message: "Thành công",
        });
      } else {
        this.$msEmitter.emit("addNotice", {
          type: this.$msEnum.NoticeType.Error,
          message: "Thất bại",
        });
      }
    },
    async hide(news) {
      if (!news.NewsId) return;

      news.NewsStatus = 2;

      const response = await this.$msAxios(
        "put",
        this.$msApi.NewsApi.Put(news.NewsId),
        news
      );

      if (response?.data) {
        this.$msEmitter.emit("addNotice", {
          type: this.$msEnum.NoticeType.Success,
          message: "Thành công",
        });
      } else {
        this.$msEmitter.emit("addNotice", {
          type: this.$msEnum.NoticeType.Error,
          message: "Thất bại",
        });
      }
    },

    /**
     * Hiện menu các chức năng. vd: nhân bản, xóa, ngừng sử dụng
     * @param {event} event Event click
     * @param {Object} news là news đã hiện menu
     * Author: LeDucTiep (01/05/2023)
     */
    toggleMenuFeature(event, news) {
      // Hiển thị menu danh sách các feature
      this.$msEmitter.emit("toggleMenuFeature", event, news);
    },

    /**
     * Chuyển đến trang trước
     * Author: LeDucTiep (06/05/2023)
     */
    async previousPage() {
      // Giảm vị trí trang
      this.pageNumberSelected--;
      // danh sách sẽ tự động thay đổi khi page number selected thay đổi
      await this.loadPostList();
    },

    /**
     * Chuyển trang tiếp theo
     * Author: LeDucTiep (06/05/2023)
     */
    async nextPage() {
      // tăng vị trí trang
      this.pageNumberSelected++;
      // danh sách sẽ tự động thay đổi khi page number selected thay đổi
      await this.loadPostList();
    },
  },
};
</script>
    
    
    <style lang="scss" scoped>
.head {
  margin-top: 30px;
  display: flex;
  justify-content: space-between;
  width: 100vw;
  .left {
    display: flex;
    gap: 24px;
    align-items: center;
    padding-left: 24px;

    p span {
      font-weight: bold;
    }

    .cancel-select {
      &:disabled {
        color: #e61d1d7c;
      }

      color: #e61d1d;
      background-color: white;
      font-weight: normal;
      cursor: pointer;
    }

    .link-button {
    }
  }

  .right {
    display: flex;
    align-items: center;
    gap: 24px;
    padding-right: 24px;

    .input-search {
      position: relative;
      top: 6px;
    }

    .icon-container {
      position: relative;

      .icon-reload {
        position: absolute;
      }
    }
  }
}

.body {
  .sticky-table {
    overflow: auto;
    border: 1px solid var(--border-input-color);
    margin: 0px 15px 0px 15px;
    height: calc(100vh - 195px);
    background-color: white;

    &::-webkit-scrollbar {
      width: 10px;
      height: 10px;
    }

    /* Handle */
    &::-webkit-scrollbar-thumb {
      background: #ccc;
    }

    /* Handle on hover */
    &::-webkit-scrollbar-thumb:hover {
      background: #a7a7a7;
    }

    .table {
      th,
      td {
        .table-data {
          width: 100%;
        }

        border-right: 1px solid var(--border-input-color);

        &:nth-child(2) {
          text-align: left;
        }

        &:nth-child(3) {
          text-align: left;
        }

        &:nth-child(4) {
          text-align: left;
        }

        &:nth-child(5) {
          text-align: left;
        }

        &:nth-child(6) {
          text-align: left !important;
        }

        &:nth-child(7) {
        }

        &:nth-child(8) {
          text-align: left;
        }

        &:nth-child(9) {
          text-align: left !important;
        }
      }

      thead {
        tr {
          th {
            padding: 0px 12px;

            &:first-child {
              z-index: 19;
            }

            &:last-child {
              z-index: 19;
              right: 0px;
            }

            &:nth-child(1) {
              min-width: 45px;
            }

            &:nth-child(2) {
              min-width: 117px;
              width: 10%;
            }

            &:nth-child(3) {
              min-width: 285px;
              width: 25%;
            }

            &:nth-child(4) {
              min-width: 120px;
              width: 30%;
            }

            &:nth-child(5) {
              min-width: 272px;
              width: 22%;
            }

            &:nth-child(6) {
              min-width: 308px;
              width: 20%;
            }

            &:nth-child(7) {
              min-width: 150px;
            }

            &:nth-child(8) {
              min-width: 150px;
            }

            &:nth-child(9) {
              min-width: 154px;
            }
          }
        }
      }

      tbody {
        tr {
          td {
            &:first-child {
              position: sticky;
              left: 0px;
              padding: 0px 12px;
            }

            &:has(.link-button) {
              position: sticky;
              right: 0px;

              .flex {
                margin-right: 24px;
                align-items: center;
              }

              .link-button {
                background-color: rgba(255, 255, 255, 0);
                margin-right: -24px;
              }

              .icon-container {
                cursor: pointer;
              }
            }
          }
        }
      }
    }
  }
}

#app > div > div.head-and-content > div.cash__home > div.cash__content {
  .head,
  .body {
    background-color: white;
  }
}

.total__head {
  height: 60px;
  background-color: white;
  border-bottom: 1px solid var(--border-color);
  display: flex;
  justify-content: center;
  align-items: center;
  gap: 24px;

  .block {
    padding: 0px 6px;
    width: calc(calc(100% - 96px) / 3);
    background-color: #eceef1;
    display: flex;
    height: 24px;

    column-gap: 25px;
    align-items: center;

    .value {
      font-weight: bold;
    }

    &:nth-child(1) {
      border-left: 4px solid #00a9f2;

      .value {
        color: #0075c0;
      }
    }

    &:nth-child(2) {
      border-left: 4px solid #ff7f2c;

      .value {
        color: #0075c0;
      }
    }

    &:nth-child(3) {
      border-left: 4px solid #74cb2f;

      .value {
        color: #ed1c24;
      }
    }
  }
}

button[disabled] {
  cursor: default !important;
}
</style>
  <style>
.container .body:has(.nav) .content {
  width: 100vw !important;
}
</style>
<style scoped>
.setting {
  position: absolute;
  background: #fff !important;
  box-shadow: 0 2px 2px rgba(0, 0, 0, 0.2);
  border-radius: 5px;
  z-index: 99;
}
</style>