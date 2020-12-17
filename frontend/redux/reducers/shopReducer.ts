import { ADD_TO_CART, REMOVE_FROM_CART } from '../types/actionTypes';
import { ShopAction } from '../types/actionTypes';
import { ShopState } from '../types/stateTypes';

let initialState: ShopState = {
    cart: [],
    toPay: null
}

export function shopReducer(state = initialState, action: ShopAction): ShopState {
    switch (action.type) {
        case ADD_TO_CART:
            return {
                ...state,
                cart: [...state.cart, action.payload]
            }
        case REMOVE_FROM_CART:
            return {
                ...state,
                cart: state.cart.filter(prod => prod.id != action.payload.id)
            }
        default:
            return state;
    }
}