import { brokenImagePlaceholder } from '../../constants';
import { ActionTypes } from '../action-types/action-types';
import { Action } from '../actions/actions';

interface ImageState {
   loadedImage: string | null;
   retrievedImage: string | null;
   loading: boolean;
   error: boolean;
}

const initialState: ImageState = {
   loadedImage: null,
   retrievedImage: null,
   loading: false,
   error: false,
};

const reducer = (
   state: ImageState = initialState,
   action: Action
): ImageState => {
   switch (action.type) {
      case ActionTypes.LOAD_IMAGE:
         return { ...state, loadedImage: action.payload };
      case ActionTypes.UNLOAD_IMAGE:
         return { ...state, loadedImage: null, retrievedImage: null };
      case ActionTypes.RETRIEVE_FILTERED_IMAGE_LOADING:
         return { ...state, loading: true, retrievedImage: null };
      case ActionTypes.RETRIEVE_FILTERED_IMAGE:
         return {
            ...state,
            loading: false,
            error: false,
            retrievedImage:
               action.payload.data?.image ?? brokenImagePlaceholder,
         };
      case ActionTypes.ERROR:
         return { ...state, loading: false, retrievedImage: null, error: true };
      default:
         return { ...state };
   }
};

export default reducer;
