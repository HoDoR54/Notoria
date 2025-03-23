import React, { ReactNode } from "react";
import useResize from "../Hooks/useResize";

interface SideBarProps {
  minWidth: number;
  borderSide: "r" | "l";
  children: ReactNode;
  maxWidth?: number;
}

const SideBar: React.FC<SideBarProps> = ({
  minWidth,
  maxWidth,
  children,
  borderSide,
}) => {
  const { size, handleMouseDown } = useResize({ width: minWidth });

  return (
    <section
      className={`shadow h-full border-gray-300 relative flex flex-col overflow-y-auto overflow-x-hidden`}
      style={{
        width: `${size.width}px`,
        minWidth: `${minWidth}px`,
        maxWidth: `${maxWidth}px`,
        borderLeft:
          borderSide === "l" ? "1px solid oklch(0.872 0.01 258.338)" : "none",
        borderRight:
          borderSide === "r" ? "1px solid oklch(0.872 0.01 258.338)" : "none",
      }}
    >
      {children}

      {/* Resize handler */}
      <div
        className={`absolute top-0 ${
          borderSide === "l" ? "left-0" : "right-0"
        } h-full w-2 cursor-ew-resize`}
        onMouseDown={(e) => handleMouseDown(e.nativeEvent)}
      ></div>
    </section>
  );
};

export default SideBar;
