import React, {FunctionComponent} from 'react';
import {Product} from "./modelTypes";


const ShopContext = React.createContext(null);

export const ProvideShopContext: React.FC = ({children}) => {
    const providedShopContext: object = useProvideShopContext();
    return (
        <ShopContext.Provider value={providedShopContext}>
            {children}
        </ShopContext.Provider>
    );
}

export const useShop = () => {
    return React.useContext(ShopContext);
}

const useProvideShopContext = (): object => {
    const [cart, setCart] = React.useState<Product[]>();
    const [test, setTest] = React.useState<string>('init');
    return {
        cart, setCart, test, setTest
    }
}