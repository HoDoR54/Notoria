import { createSlice } from "@reduxjs/toolkit";

interface currentScreenState {
  isMobile: boolean;
}

const initialState: currentScreenState = {
  isMobile: true,
};

const currentScreenSlice = createSlice({
  name: "currentScreenSize",
  initialState,
  reducers: {
    setCurrentSize: (state, action) => {
      state.isMobile = action.payload;
    },
  },
});

export const { setCurrentSize } = currentScreenSlice.actions;
export default currentScreenSlice.reducer;
