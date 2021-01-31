import React from "react";
import Head from 'next/head'
import styles from '../styles/Home.module.scss';
import {
    GetStaticProps,
    GetStaticPaths,
    GetServerSideProps,
    NextPage,
    NextPageContext,
    GetServerSidePropsContext, NextComponentType
} from 'next'
import Link from 'next/link';
import {useShop} from "../utils/shop";
import Menu from '../components/Menu';

interface Props {

}

const Home: NextPage<Props> = () => {

    return (
        <div className={styles.container}>
            <Menu/>
            <section className={styles.news}></section>
        </div>
    )
}


export const getServerSideProps: GetServerSideProps<Props> = async (context: GetServerSidePropsContext) => {
    return {props: {}}
}

export default Home;