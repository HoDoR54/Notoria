import React from "react";

const PreviewItem = () => {
  return (
    <div className="rounded-md hover:bg-blue-100 px-3 my-1 cursor-pointer group border-b-gray-300 border-b-1 pb-5 pt-3">
      <h1 className="font-semibold text-xl">Title</h1>
      <span className="text-sm text-gray-700">date and time</span>
      <ul className="flex gap-1 mt-3">
        <PreviewTag text="tag" />
        <PreviewTag text="tag2" />
      </ul>
    </div>
  );
};

interface PreviewTagProps {
  text: string;
}

const PreviewTag: React.FC<PreviewTagProps> = ({ text }) => {
  return (
    <li className="text-xs text-gray-700 rounded-full py-1 px-3 border-blue-100 border-1 group-hover:bg-blue-200">
      <i className="fa-solid fa-hashtag mr-1"></i>
      <span>{text}</span>
    </li>
  );
};

export default PreviewItem;
