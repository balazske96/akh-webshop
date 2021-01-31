import React, {useEffect} from "react";
import useWindowSize from "../utils/useWindowSize";
import {mobileMenuWindowBoundaryInPixel} from "../utils/constants";
import styles from './Menu.module.scss';
import Link from 'next/link';
import Drawer from '@material-ui/core/Drawer';

export default function Menu(): React.ReactElement {

    const {width, height} = useWindowSize();

    const [isMobileView, setIsMobileView] = React.useState(() => width < mobileMenuWindowBoundaryInPixel);
    const [isMobileMenuOpened, setIsMobileMenuOpened] = React.useState(false);

    React.useEffect(() => {
        setIsMobileView(() => width < mobileMenuWindowBoundaryInPixel);
    }, [width])


    return React.useMemo(() => {

        /**
         * Mobile view menu
         */
        if (isMobileView)
            return (
                <section className={styles.mobileMenu}>
                    <div onClick={() => setIsMobileMenuOpened(true)}>open</div>
                    <Drawer open={isMobileMenuOpened} onClose={() => setIsMobileMenuOpened(false)}>
                        <div className={styles.mobileMenuBody}>
                            <nav>
                                <Link href="#">
                                    Rólunk
                                </Link>
                                <Link href="#">
                                    Webshop
                                </Link>
                            </nav>
                        </div>
                    </Drawer>
                </section>
            )

        /**
         * Desktop view menu
         */
        return (
            <section className={styles.desktopMenu}>
                <nav>
                    <Link href="#">
                        Rólunk
                    </Link>
                    <Link href="#">
                        Shop
                    </Link>
                </nav>
            </section>
        )
    }, [isMobileView, isMobileMenuOpened])


}