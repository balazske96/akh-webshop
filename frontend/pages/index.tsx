import Head from 'next/head'
import styles from '../styles/Home.module.css'
import store from '../redux/store';
import { GetStaticProps, GetStaticPaths, GetServerSideProps } from 'next'
import { useDispatch } from 'react-redux';
import { AddNewProduct } from '../redux/actionCreators/shopActionCreators';
import Link from 'next/link';

export default function Home() {
  const dispatch = useDispatch();

  return (
    <div className={styles.container}>
      <Link href="/another-page">
        another page
      </Link>
      <button onClick={() => {
        dispatch(AddNewProduct({
          id: "1231239s",
          name: "paolo",
          displayName: "jao paolo"
        }))
      }}>Add to cart</button>
    </div>
  )
}

export const getServerSideProps: GetServerSideProps = async (context) => {
  const reduxStore = store;

  return { props: { initialReduxState: reduxStore.getState() } }
}