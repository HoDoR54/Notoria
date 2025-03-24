import { useSelector } from "react-redux";
import Controls from "./Controls";
import { RootState } from "../../Redux/store";

interface TempNoteType {
  title: string;
  body: string;
  createdAt: Date;
  updatedAt: Date;
  tags: string[];
}

let currentNote: TempNoteType | undefined = {
  title: "Learn TypeScript",
  body: "Review TypeScript interfaces and practice using type annotations.",
  createdAt: new Date(),
  updatedAt: new Date(),
  tags: ["typescript", "learning", "programming"],
};

const CurrentNote = () => {
  const { currentLanguage } = useSelector((state: RootState) => state.language);
  const { isMobile } = useSelector((state: RootState) => state.currentScreen);

  if (!currentNote) {
    return (
      <section className="grid items-center justify-center flex-1">
        <div className={`flex flex-col items-center justify-center`}>
          <h1
            className={`text-3xl font-extrabold text-gray-400 ${
              currentLanguage === "M" && "mb-3"
            }`}
          >
            {currentLanguage === "E"
              ? "Welcome to Notoria"
              : "Notoria မှ ကြိုဆိုပါတယ်"}
          </h1>
          <p className="text-gray-300 text-base font-bold">
            {currentLanguage === "E"
              ? "Designed and developed by Hpone Tauk Nyi"
              : "ဘုန်းတောက်ညီမှ ဒီဇိုင်းဆွဲပြီး တည်ဆောက်ထားပါတယ်"}
          </p>
          <p className="text-gray-300 text-base font-bold">
            hponetaukyou@gmail.com
          </p>
        </div>
      </section>
    );
  }

  return (
    <section className="flex flex-col flex-1">
      <div className="flex flex-1 w-full">
        <div className="flex flex-col flex-1">
          <div className="flex items-center pt-3 pb-5 px-2 relative">
            {/* note details */}
            <div
              className={`flex flex-col pr-10 relative gap-2 group pl-5 ${
                isMobile ? "w-full" : "max-w-fit"
              }`}
            >
              <h1 className={`font-bold ${isMobile ? "text-xl" : "text-2xl"}`}>
                {currentNote.title}
              </h1>
              <span
                className={`text-gray-500 ${isMobile ? "text-xs" : "text-sm"}`}
              >
                <i className="fa-solid fa-tags mr-2"></i>
                {currentLanguage === "E" ? "Tags: " : "တက်ဂ်များ: "}{" "}
                {currentNote.tags.join(", ")}
              </span>
              <span
                className={`text-gray-500 ${isMobile ? "text-xs" : "text-sm"}`}
              >
                <i className="fa-regular fa-clock mr-2"></i>
                {currentLanguage === "E"
                  ? "last updated on: "
                  : "နောက်ဆုံးပြင်ခဲ့သောနေ့: "}
                {currentNote.updatedAt.toDateString()}
              </span>

              <div className="absolute right-1 top-1 flex gap-2">
                <i
                  className={`text-blue-950 fa-solid fa-pen-to-square transition-opacity duration-200 cursor-pointer ${
                    isMobile
                      ? "opacity-80 hover:opacity-100"
                      : "group-hover:opacity-100 opacity-0"
                  }`}
                ></i>
                <i
                  className={`text-blue-950 fa-solid fa-download transition-opacity duration-200 cursor-pointer ${
                    isMobile
                      ? "opacity-80 hover:opacity-100"
                      : "group-hover:opacity-100 opacity-0"
                  }`}
                ></i>
              </div>
            </div>

            <div className="absolute bottom-0 left-3 right-3 h-[1px] bg-gray-200"></div>
          </div>

          <textarea
            name=""
            id=""
            className={`resize-none flex-1 focus:outline-none py-3 px-5 ${
              isMobile && "text-sm"
            }`}
            value={currentNote.body}
          ></textarea>
        </div>
      </div>

      {/* note control bar */}
      <Controls />
    </section>
  );
};

export default CurrentNote;
