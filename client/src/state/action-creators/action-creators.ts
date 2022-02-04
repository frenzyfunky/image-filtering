import axios from 'axios';
import { Dispatch } from 'redux';
import { RootState } from '..';
import { ActionTypes } from '../action-types/action-types';
import { Action, ApiParameters, ApiResponse } from '../actions/actions';

export const loadImage = (base64Image: string): Action => {
   return {
      type: ActionTypes.LOAD_IMAGE,
      payload: base64Image,
   };
};

export const unloadImage = (): Action => {
   return {
      type: ActionTypes.UNLOAD_IMAGE,
   };
};

export const retrieveFilteredImage =
   (params: ApiParameters) =>
   async (dispatch: Dispatch<Action>, getState: () => RootState) => {
      let loadedImage = getState().imageFiltering.loadedImage;
      const lastIndex = loadedImage?.lastIndexOf(',')! + 1;
      loadedImage = loadedImage?.substring(lastIndex!) as string;

      dispatch({
         type: ActionTypes.RETRIEVE_FILTERED_IMAGE_LOADING,
      });

      try {
         const response = await axios.post<ApiResponse>(
            'https://localhost:5001/api/filter',
            { ...params, image: loadedImage }
         ); //TODO change URL

         dispatch({
            type: ActionTypes.RETRIEVE_FILTERED_IMAGE,
            payload: response.data,
         });
      } catch (error) {
         dispatch({
            type: ActionTypes.ERROR,
         });
      }
   };
