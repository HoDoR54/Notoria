import { configureStore } from "@reduxjs/toolkit";
import themeReducer from "./slices/themeSlice";
import uiReducer from "./slices/uiSlice";
import currentUserReducer from "./slices/currentUserSlice";

export const store = configureStore({
  reducer: {
    theme: themeReducer,
    ui: uiReducer,
    currentUser: currentUserReducer,
  },
});

export type RootState = ReturnType<typeof store.getState>;
export type AppDispatch = typeof store.dispatch;
