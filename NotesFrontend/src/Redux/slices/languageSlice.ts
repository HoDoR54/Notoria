import { createSlice } from "@reduxjs/toolkit";

interface languageState {
  currentLanguage: "E" | "M";
}

const initialState: languageState = {
  currentLanguage: "E",
};

const languageSlice = createSlice({
  name: "language",
  initialState,
  reducers: {
    toggleLanguage: (state) => {
      state.currentLanguage = state.currentLanguage === "E" ? "M" : "E";
    },
  },
});

export const { toggleLanguage } = languageSlice.actions;
export default languageSlice.reducer;
