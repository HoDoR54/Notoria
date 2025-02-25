import { createSlice } from "@reduxjs/toolkit";
import { UserResponse } from "../../Types/userFormTypes";

interface currentUserState {
  currentUser?: UserResponse;
}

const initialState: currentUserState = {
  currentUser: undefined,
};

const currentUserSlice = createSlice({
  name: "currentUser",
  initialState,
  reducers: {
    setCurrentUser: (state, action) => {
      state.currentUser = action.payload;
    },
    removeCurrentUser: (state) => {
      state.currentUser = undefined;
    },
  },
});

export const { setCurrentUser, removeCurrentUser } = currentUserSlice.actions;
export default currentUserSlice.reducer;
