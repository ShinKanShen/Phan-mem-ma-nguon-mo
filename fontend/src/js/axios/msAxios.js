import axios from "axios";
import processError from "./axiosError";

const msAxios = async (method, api, data, headers = null) => {
  let response = null;
  try {
    switch (method.toLowerCase()) {
      case "post":
        response = await axios.post(api, data, headers);
        break;
      case "get":
        response = await axios.get(api, data, headers);
        break;
      case "put":
        response = await axios.put(api, data, headers);
        break;
      case "delete":
        response = await axios.delete(api, data, headers);
        break;
    }
  } catch (error) {
    // Xử lý lỗi
    try {
      processError(error.response);
    } catch (error) {
      console.log("processError: ", error);
    }
  }
  return response;
};

export default msAxios;
