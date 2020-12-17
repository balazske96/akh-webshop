import { Product } from '../../types/modelTypes';

export const ADD_TO_CART = 'ADD_TO_CART';
export const REMOVE_FROM_CART = 'REMOVE_FROM_CART';

export interface AddToCart {
    type: string,
    payload: Product
}

export interface RemoveFromCart {
    type: string,
    payload: Product
}

export type ShopAction = AddToCart | RemoveFromCart;