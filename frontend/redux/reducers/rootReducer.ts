import { combineReducers } from '@reduxjs/toolkit';
import { shopReducer } from './shopReducer';

export const rootReducer = combineReducers({
    shopReducer: shopReducer
})