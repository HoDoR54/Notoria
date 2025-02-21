import { createSlice } from "@reduxjs/toolkit";
import { Theme } from "../../Types/preferenceTypes";

interface ThemeState {
  value: Theme;
}

const initialState: ThemeState = {
  value: Theme.Light,
};

const themeSlice = createSlice({
  name: "theme",
  initialState,
  reducers: {
    setTheme: (state, action) => {
      state.value = action.payload;
    },
  },
});

export const { setTheme } = themeSlice.actions;
export default themeSlice.reducer;
