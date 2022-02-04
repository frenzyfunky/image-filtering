import { combineReducers } from 'redux';
import imageFilteringReducer from './image-filtering-reducer';

const reducers = combineReducers({
   imageFiltering: imageFilteringReducer,
});

export default reducers;
