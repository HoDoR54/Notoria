export interface Preference {
  id: string;
  theme: Theme;
  fontFam: FontFam;
  fontSize?: number | null;
}

enum Theme {
  Dark = "dark",
  Light = "light",
  Happy = "happy",
  Sad = "sad",
}

enum FontFam {
  TimesNewRoman = "times new roman",
  Arial = "arial",
  Garamond = "garamond",
}
