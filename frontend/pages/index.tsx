import React from "react";
import Head from 'next/head'
import styles from '../styles/Home.module.css'
import {
    GetStaticProps,
    GetStaticPaths,
    GetServerSideProps,
    NextPage,
    NextPageContext,
    GetServerSidePropsContext
} from 'next'
import Link from 'next/link';
import {useShop} from "../utils/shop";

interface Props {
}

const Home: NextPage<Props> = () => {

    const {test, setTest} = useShop();

    return (
        <div className={styles.container}>
            <Link href="/another-page">
                Another page
            </Link>
            <input onChange={(event: React.ChangeEvent<HTMLInputElement>) => {
                console.log(event);
                setTest(event.target.value);
            }}/>
            <div>{test}</div>
        </div>
    )
}

export const getServerSideProps: GetServerSideProps<Props> = async (context: GetServerSidePropsContext) => {
    return {props: {}}

}

export default Home;