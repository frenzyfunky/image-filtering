import { ActionTypes } from '../action-types/action-types';

export interface ApiResponse {
   isSuccess: boolean;
   errorMessage: string | null;
   data?: {
      image: string;
   };
}

export enum FilterType {
   MedianFilter,
   BoxFilterSmoothing,
   BoxFilterSharpening,
   Sobel,
   WeightedMedianFilter,
   WeightedBoxFilter,
   ClippedMean,
   ClippedMeanOverWeightedMedianFilter,
   WeightedMedianOverClippedMeanFilter,
}

export interface ApiParameters {
   image: string;
   filterType: FilterType;
   kernelSize: number;
   iterationCount: number;
   clippedMeanRangeRatio?: number;
   sobelIsVertical?: boolean;
}

interface LoadImageAction {
   type: ActionTypes.LOAD_IMAGE;
   payload: string;
}

interface UnloadImageAction {
   type: ActionTypes.UNLOAD_IMAGE;
}

interface RetrieveFilteredImageAction {
   type: ActionTypes.RETRIEVE_FILTERED_IMAGE;
   payload: ApiResponse;
}

interface RetrieveFilteredImageLoadingAction {
   type: ActionTypes.RETRIEVE_FILTERED_IMAGE_LOADING;
}

interface ErrorAction {
   type: ActionTypes.ERROR;
}

export type Action =
   | LoadImageAction
   | RetrieveFilteredImageAction
   | RetrieveFilteredImageLoadingAction
   | ErrorAction
   | UnloadImageAction;
