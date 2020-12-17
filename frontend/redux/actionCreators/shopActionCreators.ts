import { ADD_TO_CART, REMOVE_FROM_CART, ShopAction } from '../types/actionTypes'
import { Product } from '../../types/modelTypes';

export function AddNewProduct(product: Product): ShopAction {
    return {
        type: ADD_TO_CART,
        payload: product
    }
}

export function RemoveProductFromCart(product: Product): ShopAction {
    return {
        type: REMOVE_FROM_CART,
        payload: product
    }
}
