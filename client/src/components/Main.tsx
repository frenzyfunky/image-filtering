import { useRef } from 'react';
import { useAppDispatch, useAppSelector } from '../hooks/redux-hooks';
import {
   loadImage,
   unloadImage,
} from '../state/action-creators/action-creators';
import { FileDrop } from 'react-file-drop';
import MoonLoader from 'react-spinners/MoonLoader';

const Main = () => {
   const inputFile: any = useRef(null);
   const dispatch = useAppDispatch();
   const state = useAppSelector((state) => state.imageFiltering);

   const onButtonClick = () => {
      inputFile.current.click();
   };

   const getBase64 = (file: any) => {
      return new Promise<string>((resolve) => {
         let baseURL: string | null | ArrayBuffer;
         let reader = new FileReader();

         reader.readAsDataURL(file);

         reader.onload = () => {
            baseURL = reader.result;
            resolve(baseURL?.toString() as string);
         };
      });
   };

   return (
      <>
         {state.loading && (
            <div className='absolute top-0 bottom-0 left-0 right-0 bg-gray-400 opacity-90 z-50 flex justify-center items-center'>
               <MoonLoader color='green' loading={true} size={150} />
            </div>
         )}
         <div className='flex md:flex-row flex-col space-x-0 md:space-x-20 w-full h-full justify-center items-center relative'>
            {!state.loadedImage ? (
               <FileDrop
                  onDrop={async (files, event) => {
                     {
                        if (files?.length ?? 0 > 0) {
                           const file = await getBase64(files![0]);
                           dispatch(loadImage(file));
                        }
                     }
                  }}
               >
                  <div
                     id='file-upload'
                     className='w-96 h-96 border-dashed border-2 border-gray-400 flex flex-col justify-center items-center px-4 py-4'
                  >
                     <div className='w-20'>
                        <img src='/assets/images/drop_files.png' alt='' />
                     </div>
                     <h4 className='text-gray-400 mt-4 text-center'>
                        Resmi buraya sürükleyin veya bilgisayarınızdan seçin
                     </h4>
                     <button
                        className='bg-gray-400 px-2 py-1 mt-6 rounded font-semibold hover:bg-gray-500 transition'
                        onClick={onButtonClick}
                     >
                        Dosya Seç
                     </button>
                     <input
                        type='file'
                        id='file'
                        ref={inputFile}
                        accept='.png,.jpg,.jpeg'
                        style={{ display: 'none' }}
                        onChange={async (event) => {
                           if (event.target.files?.length ?? 0 > 0) {
                              const file = await getBase64(
                                 event.target.files![0]
                              );
                              dispatch(loadImage(file));
                           }
                        }}
                     />
                  </div>
               </FileDrop>
            ) : (
               <div className='h-3/4 flex flex-col items-center justify-center'>
                  <h4 className='text-white mb-3'>Orijinal Resim</h4>
                  <div className='max-w-3xl h-full max-h-96'>
                     <img
                        className='max-h-full max-w-full'
                        src={state.loadedImage}
                        alt='loaded-image'
                     />
                  </div>
                  <div className='absolute bottom-10 left-10'>
                     <button
                        className='bg-red-700 hover:bg-red-800 transition text-white px-2 py-2 rounded'
                        onClick={() => dispatch(unloadImage())}
                     >
                        Resmi kaldır
                     </button>
                  </div>
               </div>
            )}
            {state.retrievedImage && (
               <div className='h-3/4 flex flex-col items-center justify-center'>
                  <h4 className='text-white mb-3'>Filtrelenmiş Resim</h4>
                  <div className='max-w-3xl h-full max-h-96'>
                     <img
                        className='max-h-full max-w-full'
                        src={
                           state.loadedImage!.substring(
                              0,
                              state.loadedImage!.lastIndexOf(',') + 1
                           ) + state.retrievedImage
                        }
                        alt='loaded-image'
                     />
                  </div>
               </div>
            )}
         </div>
      </>
   );
};

export default Main;
