import {Provider} from "react-redux";
import "../styles/globals.scss";
import {ProvideShopContext} from "../utils/shop";

function MyApp({Component, pageProps}) {
    return (
        <ProvideShopContext>
            <Component {...pageProps} />
        </ProvideShopContext>
    );
}

export default MyApp;
