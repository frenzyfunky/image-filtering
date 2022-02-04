import type { NextPage } from 'next';
import Main from '../src/components/Main';
import SideBar from '../src/components/SideBar';

const Home: NextPage = () => {
   return (
      <div className='w-screen h-screen grid grid-cols-12'>
         <div className='lg:col-span-2 col-span-4 h-full bg-indigo-800'>
            <SideBar />
         </div>
         <div className='h-full w-full lg:col-span-10 col-span-8 bg-indigo-900'>
            <Main />
         </div>
      </div>
   );
};

export default Home;
