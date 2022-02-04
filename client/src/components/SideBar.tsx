import { Field, Form, useForm, useFormState } from 'react-final-form';
import { useAppDispatch, useAppSelector } from '../hooks/redux-hooks';
import { retrieveFilteredImage } from '../state/action-creators/action-creators';
import { FilterType } from '../state/actions/actions';

const SideBar = () => {
   const dispatch = useAppDispatch();
   const state = useAppSelector((state) => state.imageFiltering);

   const onSubmit = (values: any) => {
      console.log(values);
      dispatch(
         retrieveFilteredImage({
            image: '',
            iterationCount: parseInt(values.iterationCount),
            filterType: parseInt(values.filterType),
            kernelSize: parseInt(values.kernelSize),
            sobelIsVertical: values.isVertical === 'true',
         })
      );
   };

   return (
      <div className='flex flex-col py-8 px-6'>
         <div id='logo' className='w-full flex justify-center'>
            <div className='w-32'>
               <img src='/assets/images/logo_filter.png' alt='logo' />
            </div>
         </div>
         <div id='form' className='mt-12'>
            <Form
               onSubmit={onSubmit}
               validate={() =>
                  !state.loadedImage
                     ? { image: 'Lütfen resim ekleyin' }
                     : undefined
               }
               render={({ handleSubmit, values, errors, submitFailed }) => {
                  console.log(values);

                  return (
                     <form onSubmit={handleSubmit} className='w-full space-y-6'>
                        <div>
                           <label className='text-white font-semibold block mb-2'>
                              Filtre Tipi
                           </label>
                           <Field
                              validate={(value) =>
                                 !value ? 'Filtre tipi seçiniz' : undefined
                              }
                              name='filterType'
                              render={({ input, meta }) => (
                                 <div>
                                    <select
                                       className='bg-gray-700 text-white h-8 w-full rounded px-3'
                                       defaultValue=''
                                       onChange={(value) =>
                                          input.onChange(value)
                                       }
                                    >
                                       <option disabled value=''>
                                          Filtre seçiniz
                                       </option>
                                       <option value={0}>Medyan Filtre</option>
                                       <option value={1}>
                                          Box Filtre Yumuşatma
                                       </option>
                                       <option value={2}>
                                          Box Filtre Keskinleştirme
                                       </option>
                                       <option value={3}>Sobel</option>
                                       <option value={4}>
                                          Ağırlıklı Medyan
                                       </option>
                                       <option value={5}>
                                          Ağırlıklı Box Filtre
                                       </option>
                                       <option value={6}>Clipped-Mean</option>
                                       <option value={7}>
                                          Clipped-Mean+Ağırlıklı Medyan
                                       </option>
                                       <option value={8}>
                                          Ağırlıklı Medyan+Clipped-Mean
                                       </option>
                                    </select>
                                    {meta.error && meta.touched && (
                                       <span className='text-red-500'>
                                          {meta.error}
                                       </span>
                                    )}
                                 </div>
                              )}
                           />
                        </div>
                        {values['filterType'] === '3' && (
                           <div>
                              <label className='text-white font-semibold block mb-2'>
                                 Mod
                              </label>
                              <Field
                                 type='radio'
                                 name='isVertical'
                                 defaultValue='true'
                                 value='true'
                                 render={({ input }) => (
                                    <div>
                                       <input
                                          id='vertical'
                                          name={input.name}
                                          type='radio'
                                          value='true'
                                          checked={input.checked}
                                          onChange={input.onChange}
                                       />
                                       <label
                                          htmlFor='vertical'
                                          className='ml-1 text-white'
                                       >
                                          Dikey
                                       </label>
                                    </div>
                                 )}
                              />
                              <Field
                                 type='radio'
                                 name='isVertical'
                                 value='false'
                                 render={({ input }) => (
                                    <>
                                       <input
                                          id='horizontal'
                                          name={input.name}
                                          type='radio'
                                          value='false'
                                          checked={input.checked}
                                          onChange={input.onChange}
                                       />
                                       <label
                                          htmlFor='horizontal'
                                          className='ml-1 text-white'
                                       >
                                          Yatay
                                       </label>
                                    </>
                                 )}
                              />
                           </div>
                        )}
                        <div>
                           <label className='text-white font-semibold block mb-2'>
                              Filtre Boyutu
                           </label>
                           <Field
                              validate={(value) =>
                                 !value ? 'Filtre boyutu seçiniz' : undefined
                              }
                              name='kernelSize'
                              render={({ input, meta }) => (
                                 <div>
                                    <select
                                       className='bg-gray-700 text-white h-8 w-full rounded px-3'
                                       defaultValue=''
                                       onChange={(value) =>
                                          input.onChange(value)
                                       }
                                    >
                                       <option disabled value=''>
                                          Boyut seçiniz
                                       </option>
                                       <option value={3}>3x3</option>
                                       <option value={5}>5x5</option>
                                       <option value={7}>7x7</option>
                                       <option value={9}>9x9</option>
                                    </select>
                                    {meta.error && meta.touched && (
                                       <span className='text-red-500'>
                                          {meta.error}
                                       </span>
                                    )}
                                 </div>
                              )}
                           />
                        </div>
                        {values['filterType'] !== '3' && (
                           <div>
                              <label className='text-white font-semibold block mb-2'>
                                 İterasyon Sayısı
                              </label>
                              <Field
                                 defaultValue={1}
                                 name='iterationCount'
                                 render={({ input, meta }) => (
                                    <input
                                       type={'number'}
                                       className='bg-gray-700 text-white h-8 w-full rounded px-3'
                                       defaultValue={1}
                                       min={1}
                                       max={5}
                                       onChange={(value) =>
                                          input.onChange(value)
                                       }
                                    ></input>
                                 )}
                              />
                           </div>
                        )}
                        <button
                           type='submit'
                           className='text-white bg-gray-500 hover:bg-gray-700 transition px-4 py-1 rounded block'
                        >
                           Gönder
                        </button>
                        {errors && submitFailed && (
                           <span className='text-red-500'>{errors.image}</span>
                        )}
                     </form>
                  );
               }}
            />
         </div>
      </div>
   );
};

export default SideBar;
