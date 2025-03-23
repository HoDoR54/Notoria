import { useCallback, useState } from "react";

interface UseResizeParams {
  width: number;
}

const useResize = ({ width }: UseResizeParams) => {
  const [size, setSize] = useState<UseResizeParams>({ width });

  const handleMouseDown = useCallback(
    (e: MouseEvent) => {
      e.preventDefault();
      const startX = e.clientX;
      const startWidth = size.width;

      const handleMouseMove = (moveEvent: MouseEvent) => {
        setSize({
          width: startWidth + (moveEvent.clientX - startX),
        });
      };

      const handleMouseUp = () => {
        document.removeEventListener("mousemove", handleMouseMove);
        document.removeEventListener("mouseup", handleMouseUp);
      };

      document.addEventListener("mousemove", handleMouseMove);
      document.addEventListener("mouseup", handleMouseUp);
    },
    [size.width]
  );

  return { size, handleMouseDown };
};

export default useResize;
