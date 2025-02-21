import { NavigateFunction } from "react-router-dom";

export const goToRoute = (
  route: string,
  data: unknown,
  navigate: NavigateFunction
) => {
  navigate(route, { state: data });
};
