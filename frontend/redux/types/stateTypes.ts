import { Product } from '../../types/modelTypes';

export interface ShopState {
    cart: Product[],
    toPay: number | null
}
