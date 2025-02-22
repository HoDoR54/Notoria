import { createSlice } from "@reduxjs/toolkit";

interface logInStatusState {
  isLoggedIn: boolean;
}

const initialState: logInStatusState = {
  isLoggedIn: false,
};

const logInStatusSlice = createSlice({
  name: "logInStatus",
  initialState,
  reducers: {
    setIsLoggedIn: (state, action: { payload: boolean }) => {
      state.isLoggedIn = action.payload;
    },
  },
});

export const { setIsLoggedIn } = logInStatusSlice.actions;
export default logInStatusSlice.reducer;
