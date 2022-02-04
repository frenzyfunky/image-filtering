import { TypedUseSelectorHook, useDispatch, useSelector } from 'react-redux';
import { ThunkDispatch } from 'redux-thunk';
import { RootState, AppDispatch } from '../state';
import { Action } from '../state/actions/actions';

export const useAppDispatch = () =>
   useDispatch<ThunkDispatch<RootState, unknown, Action>>();
export const useAppSelector: TypedUseSelectorHook<RootState> = useSelector;