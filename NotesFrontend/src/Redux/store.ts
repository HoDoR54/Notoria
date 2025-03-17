import { configureStore } from "@reduxjs/toolkit";
import themeReducer from "./slices/themeSlice";
import uiReducer from "./slices/uiSlice";
import currentUserReducer from "./slices/currentUserSlice";
import currentScreenReducer from "./slices/currentScreen";
import languageReducer from "./slices/languageSlice";

export const store = configureStore({
  reducer: {
    theme: themeReducer,
    ui: uiReducer,
    currentUser: currentUserReducer,
    currentScreen: currentScreenReducer,
    language: languageReducer,
  },
});

export type RootState = ReturnType<typeof store.getState>;
export type AppDispatch = typeof store.dispatch;
