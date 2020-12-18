import Head from 'next/head'
import styles from '../../styles/Home.module.css'
import {GetStaticProps, GetStaticPaths, GetServerSideProps} from 'next'
import {useDispatch} from 'react-redux';
import Link from 'next/link';
import {useShop} from "../../utils/shop";
import React from "react";

export default function Home() {

    const {test, setTest} = useShop();

    return (
        <div className={styles.container}>
            <Link href="/">
                Home
            </Link>
            <input onChange={(event: React.ChangeEvent<HTMLInputElement>) => {
                console.log(event);
                setTest(event.target.value);
            }}/>
            <div>{test}</div>
        </div>
    )
}

export const getServerSideProps: GetServerSideProps = async (context) => {

    return {
        props: {}
    }
}